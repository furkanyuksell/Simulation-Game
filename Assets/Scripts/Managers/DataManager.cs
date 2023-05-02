using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DataManager : MonoBehaviour, IProvidable
{
    public TileData ColdWater;
    public TileData Desert;
    public TileData Hill;
    public TileData Plain;
    public TileData RainForest;
    public TileData Sand;
    public TileData Snow;
    public TileData Taiga;
    public TileData Water;
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
        Plain.tilePositions = new List<Vector3Int>(); 
        RainForest.tilePositions = new List<Vector3Int>();
        Sand.tilePositions = new List<Vector3Int>();
        Snow.tilePositions = new List<Vector3Int>();
        Taiga.tilePositions = new List<Vector3Int>();
        Water.tilePositions = new List<Vector3Int>();
    }
}
