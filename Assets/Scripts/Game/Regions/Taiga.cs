﻿using UnityEngine;

namespace Game.Regions
{
    public class Taiga : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Taiga;
            base.Init();
        }
    }
}