using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainForestArea : Region
{
    protected override void Start()
    {
        InitalizeRegion();
        base.Start();
    }

    void InitalizeRegion()
    {
        tileData = ServiceProvider.GetDataManager.RainForests;
    }

}
