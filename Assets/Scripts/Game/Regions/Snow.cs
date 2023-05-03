using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : Region
{
    private void Start()
    {
        Init();        
    }

    protected override void Init()
    {
        tileData = ServiceProvider.GetDataManager.Snow;
        base.Init();
    }

}
