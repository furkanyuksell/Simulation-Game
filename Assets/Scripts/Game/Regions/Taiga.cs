using UnityEngine;

namespace Game.Regions
{
    public class Taiga : Region
    {
        private void Start()
        {
                 
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Taiga;
            base.Init();
        }
    }
}