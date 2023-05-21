using UnityEngine;

namespace Game.Regions
{
    public class Water : Region
    {
        private void Start()
        {
                 
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Water;
            base.Init();
        }
    }
}