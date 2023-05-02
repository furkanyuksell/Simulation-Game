using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Animal : MonoBehaviour, IPoolable<Animal>
{
    public float animalSpawnRate;
    private ObjectPool<Animal> _animalPool;
    
    [SerializeField]float timer = 5;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 5;
            ReturnToPool();
        }
    }

    public void Initialize(ObjectPool<Animal> objPool)
    {
        _animalPool = objPool;
    }

    public void ReturnToPool()
    {
        _animalPool.Release(this);
    }
}

