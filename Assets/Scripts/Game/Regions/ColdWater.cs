using UnityEngine;

namespace Game.Regions
{
    public class ColdWater : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.ColdWater;
            base.Init();
        }
    }
}