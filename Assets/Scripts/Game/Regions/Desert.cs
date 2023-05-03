using UnityEngine;

namespace Game.Regions
{
    public class Desert : Region
    {
        private void Start()
        {
            Init();        
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Desert;
            base.Init();
        }
    }
}