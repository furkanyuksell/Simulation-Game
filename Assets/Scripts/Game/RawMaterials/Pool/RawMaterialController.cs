using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RawMaterialController 
{
    private readonly Region _region;
    private readonly TileData _tileData;
    private readonly Transform _transform;
    
    public RawMaterialController(Region region, TileData tileData, Transform transform)
    {
        _region = region;
        _tileData = tileData;
        _transform = transform;
        SetRegionRawMaterials();
    }

    private void SetRegionRawMaterials()
    {
        
        var rawMaterialParent = GameObject.Instantiate(ServiceProvider.GetDataManager.EmptyParentObject);
        var parentObject = rawMaterialParent.GetComponent<NetworkObject>();
        parentObject.Spawn();
        parentObject.TrySetParent(_transform);
        rawMaterialParent.SetNewName("RawMaterials");
        
        foreach (var rawMaterialData in _tileData.rawMaterialList)
        {
            RawMaterialPool.Instance.InitRawMaterialPools(_region ,rawMaterialData.rawMaterial, parentObject.transform);
            rawMaterialData.activeCount = RawMaterialPool.Instance.GetActiveRawMaterialCount(_region, rawMaterialData);
        }
    }


    public void SpawnRawMaterial(TileData.RawMaterialType rawMaterialType, Vector3 pos)
    {
        RawMaterial rawMaterial = RawMaterialPool.Instance.GetRawMaterial(_region, rawMaterialType);
        rawMaterial.transform.position = pos;
    }
    
}
