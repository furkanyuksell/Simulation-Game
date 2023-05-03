using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class Region : MonoBehaviour
{
    [SerializeField] protected TileData tileData;
    private Animal _animal;
    protected virtual void Init()
    {
        SetRegionAnimalPool();
    }

    private void SetRegionAnimalPool()
    {
        foreach (TileData.AnimalStruct animalStruct in tileData.animalList)
        {
            AnimalPool.Instance.InitAnimalPools(animalStruct.animal);       
        }      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAnimal();
        }
    }
    
    private void SpawnAnimal()
    {
        if (tileData.tilePositions != null)
        {
            Vector3Int randPos = tileData.tilePositions[Random.Range(0, tileData.tilePositions.Count)];
            float randSpawnPercentage = Random.Range(0,100);
            Debug.Log("Region: " + tileData.name + " / " + randSpawnPercentage);
            for(int i = 0; i < tileData.animalList.Count; i++)
            {
                if (randSpawnPercentage <= tileData.animalList[i].spawnChance)
                {
                    Animal animal = AnimalPool.Instance.GetAnimal(tileData.animalList[i].animal);
                    animal.transform.position = randPos;
                }
            }
        }
    }
}
