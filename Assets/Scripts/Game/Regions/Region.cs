using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        CalculateEachAnimalTypeToTilesPosCount();
    }

    private void SetRegionAnimalPool()
    {
        foreach (TileData.AnimalType animalStruct in tileData.animalList)
        {
            AnimalPool.Instance.InitAnimalPools(animalStruct.animal, transform);       
        }      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnAnimal();
        }
    }
    
    private void SpawnAnimal1()
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

    private void SpawnAnimal()
    {
        
    }

    private void CalculateEachAnimalTypeToTilesPosCount()
    {
        for (int i = 0; i < tileData.animalList.Count; i++)
        {
            int animalCount = Mathf.RoundToInt(tileData.tilePositions.Count / (100-tileData.animalList[i].spawnChance));
            tileData.animalList[i].maxSpawnCount = animalCount;
        }
    }

    private void DebugText()
    {
        Debug.Log(tileData.name + ": " + tileData.tilePositions.Count);
    }

    private void OnEnable()
    {
        LogHelper.OnDebug += DebugText;
    }

    private void OnDisable()
    {
        LogHelper.OnDebug -= DebugText;
    }
}
