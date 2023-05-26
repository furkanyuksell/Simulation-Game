using UnityEngine;

namespace Game.Regions
{
    public class Plain : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Plain;
            base.Init();
        }
    }
}