using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public abstract class Animal : NetworkBehaviour, IPoolable<Animal>
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

