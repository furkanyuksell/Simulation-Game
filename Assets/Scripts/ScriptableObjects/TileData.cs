using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public float hungerRate;
    public float thirstRate;
    public float chillRate;
    public TileBase[] tiles;
    public List<Vector3Int> tilePositions;
    public List<AnimalStruct> animalList = new List<AnimalStruct>();

    [System.Serializable]
    public struct AnimalStruct
    {
        public string animalName;
        public Animal _animal;
        public float spawnChance;
        public float spawnTime;
        public float spawnDistance;
        public int maxSpawnCount;
    }
    
}
