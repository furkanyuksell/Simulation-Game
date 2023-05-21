using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class AnimalController
{
    private readonly TileData _tileData;
    private readonly Transform _transform;
    public AnimalController(TileData tileData, Transform transform)
    {
        _tileData = tileData;
        _transform = transform;
        SetRegionAnimalPoolAndAnimalMaxCount();
    }

    private void SetRegionAnimalPoolAndAnimalMaxCount()
    {
        foreach (var animalData in _tileData.animalList)
        {
            AnimalPool.Instance.InitAnimalPools(animalData.animal, _transform);   
            int animalCount = Mathf.RoundToInt(_tileData.tilePositions.Count / (100-animalData.spawnChance));
            animalData.maxSpawnCount = animalCount;
            animalData.activeCount = 0;
            animalData.cooldown = 0;
        }      
    }

    public Vector3Int RandTilePos { get; set; }

    public bool CanAnimalSpawn(TileData.AnimalType animalType)
    {
        //Debug.Log("Animal: " + animalType.animalName + " / Active: " + animalType.activeCount + " / Max: " + animalType.maxSpawnCount + " / Cooldown: " + animalType.cooldown);
        if (animalType.activeCount < animalType.maxSpawnCount && animalType.cooldown <= 0)
        {
            animalType.cooldown = animalType.spawnTime;
            RandTilePos = _tileData.tilePositions[UtilServices.GetRandomNumber(0, _tileData.tilePositions.Count)];
            return true;
        }
        animalType.cooldown -= Time.deltaTime+5;
        
        return false;
    }
    /*
    public Animal SpawnAnimal(TileData.AnimalType animalType)
    {
        return AnimalPool.Instance.GetAnimal(animalType);
    }*/
    
}
