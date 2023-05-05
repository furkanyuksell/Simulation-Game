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

    private AnimalController AnimalController { get; set; }

    protected virtual void Init()
    {
        InitializeRegionAnimalsAtStart();
    }

    private void InitializeRegionAnimalsAtStart()
    {
        if (tileData.animalList.Count == 0)
        {
            Debug.Log("No animals in region: " + tileData.name);
            return;
        }
        
        AnimalController = new AnimalController(tileData, transform);
        SpawnRegionAnimalPopulation();
    }
    
    private Vector3Int _randTilePos;
    private void SpawnRegionAnimalPopulation()
    {
        foreach (var animalType in tileData.animalList)
        {
            for (int i = 0; i < (animalType.maxSpawnCount/2); i++)
            {
                Animal animal = AnimalController.SpawnAnimal(animalType);
                _randTilePos = tileData.tilePositions[UtilServices.GetRandomNumber(0, tileData.tilePositions.Count)];
                animal.transform.position = _randTilePos;
            }
        }      
    }

    private void SpawnRegionAnimalWithTime()
    {
        foreach (var animalType in tileData.animalList)
        {
            
        }    
    }
    

    //Debugg---------------------------
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
