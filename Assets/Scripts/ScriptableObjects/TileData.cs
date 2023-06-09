using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public float hungerRate;
    public float thirstRate;
    public float chillRate;
    public TileBase[] tiles;
    public bool isWalkable;
    [HideInInspector] public List<Vector3Int> tilePositions;
    public List<AnimalType> animalList = new List<AnimalType>();
    public List<RawMaterialType> rawMaterialList = new List<RawMaterialType>();
    
    [System.Serializable]
    public class AnimalType
    {
        public string animalName;
        public Animal animal;
        public float spawnChance;
        public float spawnTime;
        public float cooldown;
        public int maxSpawnCount;
        public int activeCount;
    }
    
    [System.Serializable]
    public class RawMaterialType
    {
        public string rawMaterialName;
        public RawMaterial rawMaterial;
        public int maxSpawnCount;
        public int activeCount;
    }
}
