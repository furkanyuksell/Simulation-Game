using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainForest : Region
{
    protected override void Init()
    {
        tileData = ServiceProvider.GetDataManager.RainForest;
        base.Init();
    }
    
}
