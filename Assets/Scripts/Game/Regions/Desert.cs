using UnityEngine;

namespace Game.Regions
{
    public class Desert : Region
    {
        private void Start()
        {
                 
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Desert;
            base.Init();
        }
    }
}