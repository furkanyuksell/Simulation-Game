using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Animal : MonoBehaviour
{
    public float animalSpawnRate;
    private ObjectPool<Animal> _animalPool;
    public void SetPool(ObjectPool<Animal> animalPool) => _animalPool = animalPool; 
    private void Start()
    {
        
    }
    
}

