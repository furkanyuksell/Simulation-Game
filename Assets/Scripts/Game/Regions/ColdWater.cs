using UnityEngine;

namespace Game.Regions
{
    public class ColdWater : Region
    {
        private void Start()
        {
                 
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.ColdWater;
            base.Init();
        }
    }
}