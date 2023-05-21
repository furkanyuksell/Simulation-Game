using UnityEngine;

namespace Game.Regions
{
    public class Sand : Region
    {
        private void Start()
        {
                  
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Sand;
            base.Init();
        }
    }
}