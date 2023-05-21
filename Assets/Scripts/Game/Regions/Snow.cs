using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : Region
{
    protected override void Init()
    {
        tileData = ServiceProvider.GetDataManager.Snow;
        base.Init();
    }

}
