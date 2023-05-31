using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    Vector3[] path;

    private int targetIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PathRequestManager.RequestPath(transform.position,MapGenerator.Instance.tileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)), OnPathFound);
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            path = Pathfinding.Instance.FindPath(transform.position, MapGenerator.Instance.tileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            targetIndex = 0;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
            //Debug.Log(MapGenerator.Instance.tileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }*/
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, 6f * Time.deltaTime);
            yield return null;
        }
    }
}
