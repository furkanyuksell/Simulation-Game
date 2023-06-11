using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance { get; set; }
    private int _mapSizeX, _mapSizeY;
    public Node[,] grid;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateGrid(int mapSizeX, int mapSizeY)
    {
        grid = new Node[mapSizeX, mapSizeY];
        _mapSizeX = mapSizeX;
        _mapSizeY = mapSizeY;
    }

    public void FillGrid(Node node)
    {
        grid[node.GridX, node.GridY] = node;
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x + (_mapSizeX / 2));
        int y = Mathf.RoundToInt(worldPos.y + (_mapSizeY / 2));
        return grid[x, y];
    }

    public bool MapSizeControl(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x + (_mapSizeX / 2));
        int y = Mathf.RoundToInt(worldPos.y + (_mapSizeY / 2));
        if(x > 0 && x < _mapSizeX && y > 0 && y < _mapSizeY)
            return true;
        return false;
    }
    
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                
                int checkX = node.GridX + x;
                int checkY = node.GridY + y;
                
                if (checkX >= 0 && checkX < _mapSizeX && checkY >= 0 && checkY < _mapSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }
    
    public int MaxSize {
        get {
            return _mapSizeX * _mapSizeY;
        }
    }

}
