using System;
using System.Collections;
using System.Collections.Generic;
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
        }      
    }
    
    public Animal SpawnAnimal(TileData.AnimalType animalType)
    {
        return animalType.activeCount < animalType.maxSpawnCount ? AnimalPool.Instance.GetAnimal(animalType) : null;
    }
    
}
