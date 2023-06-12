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
        var animalParent = GameObject.Instantiate(ServiceProvider.GetDataManager.EmptyParentObject);
        var parentObject = animalParent.GetComponent<NetworkObject>();
        parentObject.Spawn();
        parentObject.TrySetParent(_transform);
        animalParent.SetNewName("Animals");
        
        foreach (var animalData in _tileData.animalList)
        {
            AnimalPool.Instance.InitAnimalPools(animalData.animal, parentObject.transform);   
            int animalCount = Mathf.RoundToInt(_tileData.tilePositions.Count / (100-animalData.spawnChance));
            animalData.maxSpawnCount = animalCount;
            animalData.activeCount = AnimalPool.Instance.GetActiveAnimalCount(animalData);
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
        animalType.cooldown -= Time.deltaTime;
        
        return false;
    }
    
    
    public void SpawnAnimal(TileData.AnimalType animalType, Vector3 pos)
    {
        Animal animal = AnimalPool.Instance.GetAnimal(animalType);
        animal.transform.position = pos;
        
    }
    
}
