using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public abstract class Region : NetworkBehaviour
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
        //SpawnRegionAnimalPopulation();
    }
    
    private Vector3Int _randTilePos;
   /*
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
    */
   
    private void SpawnRegionAnimalWithTime()
    {
        var i = 0;
        foreach (var animalType in tileData.animalList)
        {
            _randTilePos = tileData.tilePositions[UtilServices.GetRandomNumber(0, tileData.tilePositions.Count)];
            if (AnimalController.CanAnimalSpawn(animalType))
            {
                SetAnimalsOnClientRpc(i, _randTilePos);      
                //Debug.Log("Spawned animal: " + animalType.animal.name + " at: " + _randTilePos);
            }
            i++;
        }
    }
    [ClientRpc]
    private void SetAnimalsOnClientRpc(int animalIndex, Vector3 pos)
    {
        Animal animal = AnimalPool.Instance.GetAnimal(tileData.animalList[animalIndex]);
        animal.transform.position = pos;
    }
    


    //private float _serverCooldown = 6f;
    private void Update()
    {
        
        if (!IsServer)
            return; 
        SpawnRegionAnimalWithTime();
        /*if (!(_serverCooldown <= 0))
        {
            _serverCooldown -= Time.deltaTime;
            return;
        }*/

    }
    
    
    
    //Debug---------------------------
    private void DebugText()
    {
        Debug.Log(tileData.name + ": " + tileData.tilePositions.Count);
    }

    private void OnEnable()
    {
        LogHelper.OnDebug += DebugText;
        MapGenerator.OnMapDone += Init;
    }

    private void OnDisable()
    {
        LogHelper.OnDebug -= DebugText;
        MapGenerator.OnMapDone -= Init;
    }
}
