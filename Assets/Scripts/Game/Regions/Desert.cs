using UnityEngine;

namespace Game.Regions
{
    public class Desert : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Desert;
            base.Init();
        }
    }
}