using UnityEngine;

namespace Game.Regions
{
    public class Sand : Region
    {
        protected override void Init()
        {
            tileData = ServiceProvider.GetDataManager.Sand;
            base.Init();
        }
    }
}