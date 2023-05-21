using UnityEngine;

namespace Game.Regions
{
    public class Hill : Region
    {
        private void Start()
        {
                   
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Hill;
            base.Init();
        }
    }
}