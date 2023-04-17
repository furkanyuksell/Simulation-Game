using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population : MonoBehaviour
{
    public static Population Instance { get; private set; }
    private Tilemap _tileMap;
    
    private void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        _tileMap = MapGenerator.Instance.tileMap;
        MapGenerator.Instance.GenerateMap();
        
        CreatePopulationAccordingToTile();
    }

    private void CreatePopulationAccordingToTile()
    {
        
    }

}


