using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainForestArea : Region
{
    
    void Start()
    {
        _tileData = ServiceProvider.GetDataManager.RainForests;    
    }
    
}
