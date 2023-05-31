using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; set; }
    //public Transform seeker, target;

    private void Awake()
    {
        Instance = this;
    }
    
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        
        Node startNode = Grid.Instance.NodeFromWorldPoint(startPos);
        Node targetNode = Grid.Instance.NodeFromWorldPoint(targetPos);

        if (startNode.IsWalkable && targetNode.IsWalkable)
        {
            Heap<Node> openSet = new Heap<Node>(Grid.Instance.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
            
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }
                foreach (Node neighbor in Grid.Instance.GetNeighbours(currentNode))
                {
                    if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                        continue;
            
                    int newCostToNeighbor = currentNode.G + GetDistance(currentNode, neighbor);
                    if (newCostToNeighbor < neighbor.G || !openSet.Contains(neighbor))
                    {
                        neighbor.G = newCostToNeighbor;
                        neighbor.H = GetDistance(neighbor, targetNode);
                        neighbor.Parent = currentNode;

                        if (!openSet.Contains(neighbor)) 
                            openSet.Add(neighbor);
                    }
                }
            }
        }
        
        if (pathSuccess) {
            waypoints = RetracePath(startNode,targetNode);
        }
        PathRequestManager.Instance.FinishedProcessingPath(waypoints,pathSuccess);
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
    
    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
		
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
		
    }
    
    Vector3[] SimplifyPath(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
		
        for (int i = 1; i < path.Count; i ++) {
            Vector2 directionNew = new Vector2(path[i-1].GridX - path[i].GridX,path[i-1].GridY - path[i].GridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i].Position);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }
}
