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
    private RawMaterialController RawMaterialController { get; set; }

    protected virtual void Init()
    {
        InitializeRegionAnimalsAtStart();
        InitializeRegionRawMaterialsAtStart();
    }
    private void InitializeRegionRawMaterialsAtStart()
    {
        if (tileData.rawMaterialList.Count == 0)
        {
            Debug.Log("No raw materials in region: " + tileData.name);
            return;
        }
        
        RawMaterialController = new RawMaterialController(this, tileData, transform);
        SpawnRegionRawMaterialPopulation();
    }

    private void SpawnRegionRawMaterialPopulation()
    {
        var index = 0;
        foreach (var rawMaterialType in tileData.rawMaterialList)
        {
            for (int i = 0; i < (rawMaterialType.maxSpawnCount/2); i++)
            {
                _randTilePos = tileData.tilePositions[UtilServices.GetRandomNumber(0, tileData.tilePositions.Count)]; 
                SetRawMaterialsOnClientRpc(index, _randTilePos);
            }

            index++;
        }      
    }

    [ClientRpc]
    private void SetRawMaterialsOnClientRpc(int index, Vector3Int randTilePos)
    {
        RawMaterialController.SpawnRawMaterial(tileData.rawMaterialList[index], randTilePos);
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
        var index = 0;
        foreach (var animalType in tileData.animalList)
        {
            for (int i = 0; i < (animalType.maxSpawnCount/2); i++)
            {
               _randTilePos = tileData.tilePositions[UtilServices.GetRandomNumber(0, tileData.tilePositions.Count)]; 
               SetAnimalsOnClientRpc(index, _randTilePos);
            }

            index++;
        }      
    }
    
    private void SpawnRegionAnimalWithTime()
    {
        var i = 0;
        foreach (var animalType in tileData.animalList)
        {
            _randTilePos = tileData.tilePositions[UtilServices.GetRandomNumber(0, tileData.tilePositions.Count)];
            if (AnimalController.CanAnimalSpawn(animalType))
            {
                SetAnimalsOnClientRpc(i, AnimalController.RandTilePos);
            }
            i++;
        }
    }
    
    [ClientRpc]
    private void SetAnimalsOnClientRpc(int animalIndex, Vector3 pos)
    {
        AnimalController.SpawnAnimal(tileData.animalList[animalIndex], pos);
    }
    


    private float _serverCooldown = 6f;
    private void Update()
    {
        if (!IsServer)
            return;
        if (!(_serverCooldown <= 0))
        {
            _serverCooldown -= Time.deltaTime;
            return;
        }
        SpawnRegionAnimalWithTime();
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
