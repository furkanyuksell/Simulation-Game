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
    protected virtual void Start()
    {
        InitalizeRegion();
    }

    private void InitalizeRegion()
    {
        CalculateAnimalSpawnChance();
        SpawnAnimal();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAnimal();
        }
    }

    void SpawnAnimal()
    {
        Vector3Int randPos = tileData.tilePositions[Random.Range(0, tileData.tilePositions.Count)];
        _animal = PickAnimal();
        if (_animal != null)
        {
            Instantiate(_animal, randPos, Quaternion.identity);
            AnimalPool.Instance.GetAnimal(_animal);
        }
    }
    private Animal PickAnimal()
    {
        float randSpawnPercentage = Random.Range(0,100);
        float cumulativeProbability = 0f;
        for(int i = 0; i < tileData.animalList.Count; i++)
        {
            cumulativeProbability += tileData.animalList[i].animal.animalSpawnRate;
            if (randSpawnPercentage <= cumulativeProbability)
            {
                return tileData.animalList[i].animal;
            }
        }

        return null;
    }
    
    float _totalSpawnChance = 0;
    void CalculateAnimalSpawnChance()
    {
        //calculate spawn chance for each animal
        
        foreach (var animalStruct in tileData.animalList)
        {
            _totalSpawnChance += animalStruct.spawnChance;
        }

        foreach (var animalStruct in tileData.animalList)
        {
            float spawnChancePercentage = animalStruct.spawnChance / _totalSpawnChance * 100f;
            animalStruct.animal.animalSpawnRate = spawnChancePercentage;
        }
    }
}
