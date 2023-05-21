using UnityEngine;

namespace Game.Regions
{
    public class Hill : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Hill;
            base.Init();
        }
    }
}