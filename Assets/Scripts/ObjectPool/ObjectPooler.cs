using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler<T> where T : MonoBehaviour, IPoolable<T>
{
    private GameObject prefab;
    private Transform parent;
    
    public ObjectPool<T> Pool;

    public ObjectPooler(GameObject prefab, Transform regionParent)
    {
        this.prefab = prefab;

        var emptyParentObject = GameObject.Instantiate(ServiceProvider.GetDataManager.EmptyParentObject);
        var parentObject = emptyParentObject.GetComponent<NetworkObject>();
        parentObject.Spawn();
        parentObject.TrySetParent(regionParent);
        emptyParentObject.SetNewName(prefab.name + " Pool");

        parent = parentObject.transform;
        Pool = new ObjectPool<T>(CreateObject, OnGetFromPool, OnReturnToPool);
    }

    private void OnReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGetFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
    }
    
    private T CreateObject()
    {
        T t;
        t = GameObject.Instantiate(prefab).GetComponent<T>();
        var networkObject = t.gameObject.GetComponent<NetworkObject>();
        networkObject.Spawn();
        networkObject.TrySetParent(parent);
        /*t.GetComponent<NetworkObject>().Spawn();
        t.GetComponent<NetworkObject>().TrySetParent(parent);*/
        t.gameObject.SetActive(true);
        t.Initialize(Pool);
        return t;
    }
}
