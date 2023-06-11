using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node : IHeapItem<Node>
{
    public Vector3Int Position { get; set; }
    public int GridX { get; set; }
    public int GridY { get; set; }
    public Node Parent { get; set; }
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;
    public bool IsWalkable { get; set; }
    public float HungerRate { get; set; }
    public float ThirstRate { get; set; }
    public float ChillRate { get; set; }
    int _heapIndex;

    public Node(TileData tiledata, Vector3Int position, int gridX, int gridY)
    {
        IsWalkable = tiledata.isWalkable;
        HungerRate = tiledata.hungerRate;
        ThirstRate = tiledata.thirstRate;
        ChillRate = tiledata.chillRate;
        Position = position;
        GridX = gridX;
        GridY = gridY;

    }
    public int HeapIndex {
        get {
            return _heapIndex;
        }
        set {
            _heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = F.CompareTo(nodeToCompare.F);
        if (compare == 0) {
            compare = H.CompareTo(nodeToCompare.H);
        }
        return -compare;
    }
}
