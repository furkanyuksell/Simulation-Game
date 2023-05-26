using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;

public abstract class RawMaterial : NetworkBehaviour, IPoolable<RawMaterial>
{
    [SerializeField] private List<GameObject> resources = new List<GameObject>();
    private ObjectPool<RawMaterial> _rawMaterialPool;
    public void Initialize(ObjectPool<RawMaterial> objPool)
    {
        _rawMaterialPool = objPool;
    }

    public void ReturnToPool()
    {
        _rawMaterialPool.Release(this);
    }
}
