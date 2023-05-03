using System.Collections;
using System.Collections.Generic;
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
        var parentObject = new GameObject(prefab.name + " Pool");
        parentObject.transform.parent = regionParent;
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
        t = GameObject.Instantiate(prefab, parent).GetComponent<T>();
        t.gameObject.SetActive(true);
        t.Initialize(Pool);
        return t;
    }
}
