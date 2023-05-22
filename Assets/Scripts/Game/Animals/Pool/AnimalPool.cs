using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPool : MonoBehaviour
{
    public static AnimalPool Instance { get; private set; }

    private Dictionary<Animal, ObjectPooler<Animal>> pools = new Dictionary<Animal, ObjectPooler<Animal>>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void InitAnimalPools(Animal animal, Transform regionParent)
    {
        pools.Add(animal, new ObjectPooler<Animal>(animal.gameObject, regionParent));
    }
    
    public Animal GetAnimal(TileData.AnimalType animalType)
    {
        if (pools.TryGetValue(animalType.animal, out ObjectPooler<Animal> pool))
        {
            animalType.activeCount = pool.Pool.CountActive+1;
            return pool.Pool.Get();
            /*Debug.Log(
                "Animal: " +
                animal.name +
                " / Active: " +
                pool.Pool.CountActive
                + " / Inactive: " +
                pool.Pool.CountInactive
            );*/
        }
        return null;
    }

    public int GetActiveAnimalCount(TileData.AnimalType animalType)
    {
        return pools.TryGetValue(animalType.animal, out ObjectPooler<Animal> pool) ? pool.Pool.CountActive : 0;
    }
}
