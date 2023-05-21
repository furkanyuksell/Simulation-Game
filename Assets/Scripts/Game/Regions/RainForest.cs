using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainForest : Region
{
    private void Start()
    {
        
    }

    protected override void Init()
    {
        tileData = ServiceProvider.GetDataManager.RainForest;
        base.Init();
    }
    
}
