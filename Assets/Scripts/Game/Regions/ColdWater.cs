using UnityEngine;

namespace Game.Regions
{
    public class ColdWater : Region
    {
        private void Start()
        {
            Init();        
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.ColdWater;
            base.Init();
        }
    }
}