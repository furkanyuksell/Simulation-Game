using UnityEngine;

namespace Game.Regions
{
    public class Water : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Water;
            base.Init();
        }
    }
}