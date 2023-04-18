using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IProvidable
{
    public TileData ColdWater;
    public TileData Desert;
    public TileData Hill;
    public TileData Plains;
    public TileData RainForests;
    public TileData Sand;
    public TileData Snow;
    public TileData Taiga;
    public TileData Water;
    public List<TileData> tileDataList;
    private void Awake()
    {
        Initialize();
        ServiceProvider.Register(this);    
    }
    
    public void Initialize()
    {
        ColdWater.tilePositions = new List<Vector3Int>();
        Desert.tilePositions = new List<Vector3Int>();
        Hill.tilePositions = new List<Vector3Int>();
        Plains.tilePositions = new List<Vector3Int>(); 
        RainForests.tilePositions = new List<Vector3Int>();
        Sand.tilePositions = new List<Vector3Int>();
        Snow.tilePositions = new List<Vector3Int>();
        Taiga.tilePositions = new List<Vector3Int>();
        Water.tilePositions = new List<Vector3Int>();
    }
}
