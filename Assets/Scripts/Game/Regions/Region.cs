using Unity.Netcode;
using UnityEngine;


public abstract class Region : NetworkBehaviour
{
    [SerializeField] protected TileData tileData;
    private AnimalController AnimalController { get; set; }
    private RawMaterialController RawMaterialController { get; set; }
    private bool _isInitialized;

    protected virtual void Init()
    {
        InitializeRegionAnimalsAtStart();
        InitializeRegionRawMaterialsAtStart();
        _isInitialized = true;
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
                RawMaterialController.SpawnRawMaterial(tileData.rawMaterialList[index], _randTilePos);
            }
            index++;
        }      
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
               AnimalController.SpawnAnimal(tileData.animalList[index], _randTilePos);
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
                AnimalController.SpawnAnimal(tileData.animalList[i], AnimalController.RandTilePos);
            }
            i++;
        }
    }

    
    private void Update()
    {
        if (!_isInitialized)
            return;
        
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
        MultiplayerManager.OnServerDataLoad += Init;
    }

    private void OnDisable()
    {
        LogHelper.OnDebug -= DebugText;
        MultiplayerManager.OnServerDataLoad -= Init;
    }
}