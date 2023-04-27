using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPool : MonoBehaviour
{
    public static AnimalPool Instance { get; private set; }
    Dictionary<Animal, ObjectPooler<Animal>> pools = new Dictionary<Animal, ObjectPooler<Animal>>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void GetAnimal(Animal animal)
    {
        if (pools.ContainsKey(animal))
        {
            Debug.Log(animal.name + " is in the pool");
            UsePool(animal);
        }
        else
        {
            Debug.Log(animal.name + " is not in the pool so its added");
            pools.Add(animal, new ObjectPooler<Animal>(animal.gameObject));
        }
    }
    
    public void UsePool(Animal animal)
    {
        if (pools.TryGetValue(animal, out ObjectPooler<Animal> pool))
        {
            Debug.Log("Pooldaki Obje Adi: " + pool.GetFromPool().name);
        }
    }
}
