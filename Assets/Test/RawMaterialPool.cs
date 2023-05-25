using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RawMaterialPool: MonoBehaviour
{
    public static RawMaterialPool Instance { get; private set; }
    private Dictionary<RawMaterial, ObjectPooler<RawMaterial>> pools = new();
    private Dictionary<Tuple<Region, RawMaterial>, ObjectPooler<RawMaterial>> _rawPools = new();
    private void Awake()
    {
        Instance = this;
    }

    public void InitRawMaterialPools(Region region, RawMaterial rawMaterial, Transform regionParent)
    {
        /*
        if (!pools.ContainsKey(rawMaterial))
        {
            pools.Add(rawMaterial, new ObjectPooler<RawMaterial>(rawMaterial.gameObject, regionParent));   
        }*/

        var keyToCheck = new Tuple<Region, RawMaterial>(region, rawMaterial);
        if (!_rawPools.ContainsKey(keyToCheck))
        {
            _rawPools.Add(keyToCheck, new ObjectPooler<RawMaterial>(rawMaterial.gameObject, regionParent));
        }
        
    }
    
    public RawMaterial GetRawMaterial(Region region, TileData.RawMaterialType rawMaterialType)
    {
        if (_rawPools.TryGetValue(new Tuple<Region, RawMaterial>(region, rawMaterialType.rawMaterial), out ObjectPooler<RawMaterial> rawPool))
        {
            rawMaterialType.activeCount = rawPool.Pool.CountActive+1;
            return rawPool.Pool.Get();
        }
        /*
        if (pools.TryGetValue(rawMaterialType.rawMaterial, out ObjectPooler<RawMaterial> pool))
        {
            rawMaterialType.activeCount = pool.Pool.CountActive+1;
            return pool.Pool.Get();
            /*Debug.Log(
                "RawMaterial: " +
                rawMaterial.name +
                " / Active: " +
                pool.Pool.CountActive
                + " / Inactive: " +
                pool.Pool.CountInactive
            );
        }*/
        return null;
    }

    public int GetActiveRawMaterialCount(Region region, TileData.RawMaterialType rawMaterialType)
    {
        return _rawPools.TryGetValue(new Tuple<Region, RawMaterial>(region, rawMaterialType.rawMaterial), out ObjectPooler<RawMaterial> rawPool) ? rawPool.Pool.CountActive : 0;
        //return pools.TryGetValue(rawMaterialType.rawMaterial, out ObjectPooler<RawMaterial> pool) ? pool.Pool.CountActive : 0;
    }
}
