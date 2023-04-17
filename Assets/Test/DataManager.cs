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

    private void Awake()
    {
        ServiceProvider.Register(this);
    }
}
