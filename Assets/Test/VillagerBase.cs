using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class VillagerBase : NetworkBehaviour
{
    Vector3[] _path;
    private int _targetIndex;
    public float speed = 10f;
    
    
    private float hunger = 1000;
    private float thirst = 1000;
    private float chill = 1000;
    private float breakPoint = 990;

    private short isHungry;
    private short isThirst;
    private short isChill;

    private bool isHappy = true;

    Node currentNode;
    
    public bool hasTask;
    public List<Selectables> taskList = new();
    public List<InputController.SelectableTypes> selectableTypes = new();
    
    private void Awake()
    {
        hasTask = false;
    }

    private void Update()
    {
        currentNode = Grid.Instance.NodeFromWorldPoint(transform.position);
        hunger -= currentNode.HungerRate * Time.deltaTime;
        thirst -= currentNode.ThirstRate * Time.deltaTime;
        chill -= currentNode.ChillRate *  Time.deltaTime;
        if(hunger < breakPoint)
            isHungry = 1;
        else
            isHungry = 0;
        if(thirst < breakPoint)
            isThirst = 1;
        else
            isThirst = 0;
        if(chill < breakPoint)
            isChill = 1;
        else
            isChill = 0;

        if(isHungry + isThirst + isChill > 1 && isHappy)
        {
            isHappy = false;
            TopPanelController.Instance.TurnVillagerSad();
        }
        else if(isHungry + isThirst + isChill < 2 && !isHappy)
        {
            isHappy = true;
            TopPanelController.Instance.TurnVillagerHappy();
        }

    }
    
    public void StartTask(List<Selectables> task)
    {
        hasTask = true;
        taskList = task;
        StartMovement();
    }
    
    public void StopTask()
    {
        hasTask = false;
        taskList.Clear();
    }

    private void StartMovement()
    {
        PathRequestManager.RequestPath(transform.position, taskList[0].transform.position, OnPathFound);
    }
    

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            _path = newPath;
            _targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = _path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                _targetIndex++;
                if (_targetIndex >= _path.Length)
                {
                    Debug.Log("End of path reached");
                    StopCoroutine(DoTask());
                    StartCoroutine(DoTask());
                    yield break;
                }
                currentWaypoint = _path[_targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
    
    private IEnumerator DoTask()
    {
        if (taskList[0].TryGetComponent(out RawMaterial rawMaterial))
        {
            if (rawMaterial.TryGetComponent(out IDamageable damageable))
            {
                rawMaterial.healthBarSlider.gameObject.SetActive(true);
                while (damageable.Health > 0)
                {
                    damageable.TakeDamage(5);
                    yield return new WaitForSeconds(1f);
                }       
            }
        }
        taskList.RemoveAt(0);
        if (taskList.Count > 0)
        {
            StartMovement();
        }
        else
        {
            StopTask();
        }
    }
}
