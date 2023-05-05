using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public abstract class Animal : MonoBehaviour, IPoolable<Animal>
{
    private ObjectPool<Animal> _animalPool;
    public static float Countdown { get; set; }
    [SerializeField] protected float showCountDown;
    public void Initialize(ObjectPool<Animal> objPool)
    {
        _animalPool = objPool;
    }

    private void Update()
    {
        showCountDown = Countdown;
    }

    public void ReturnToPool()
    {
        _animalPool.Release(this);
    }

}

