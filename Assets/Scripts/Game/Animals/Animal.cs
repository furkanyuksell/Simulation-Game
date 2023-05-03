using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Animal : MonoBehaviour, IPoolable<Animal>
{
    private ObjectPool<Animal> _animalPool;
    public void Initialize(ObjectPool<Animal> objPool)
    {
        _animalPool = objPool;
    }

    public void ReturnToPool()
    {
        _animalPool.Release(this);
    }
}

