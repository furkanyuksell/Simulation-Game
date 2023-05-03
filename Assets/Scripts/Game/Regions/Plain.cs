using UnityEngine;

namespace Game.Regions
{
    public class Plain : Region
    {
        private void Start()
        {
            Init();        
        }

        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Plain;
            base.Init();
        }
    }
}