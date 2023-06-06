using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public abstract class VillagerBase : NetworkBehaviour
{
    Vector3[] _path;
    private int _targetIndex;
    public float speed = 10f;
    
    public bool hasTask;
    public List<Selectables> taskList = new();
    public List<InputController.SelectableTypes> selectableTypes = new();
    
    private void Awake()
    {
        hasTask = false;
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
