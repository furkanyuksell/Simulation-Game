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

    public void InitAnimalPools(Animal animal)
    {
        pools.Add(animal, new ObjectPooler<Animal>(animal.gameObject));
    }
    
    public void GetAnimal(Animal animal)
    {
        if (pools.TryGetValue(animal, out ObjectPooler<Animal> pool))
        {
            Debug.Log("Pooldaki Obje Adi: " + pool.GetFromPool().name);
            pool.Pool.Get();
            //initAnimal.gameObject.GetComponent<Animal>().SetPool(pool.Pool);
            Debug.Log(pool.Pool.CountActive);
        }
    }
}
