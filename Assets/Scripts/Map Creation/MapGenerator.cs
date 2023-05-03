using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }
    public enum DrawMode
    {
        NoiseMap,
        ColorMap,
        TileMap,
        TemperatureMap,
        HumidityMap
    };

    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;
    [SerializeField] public TerrainType[] regions;
    [SerializeField] public Tilemap tileMap;

    private int[,] world = new int[6, 7]{{0,0,0,0,8,6,6}, // 0 snow,  // 5 water
                                         {0,0,8,8,8,5,6}, // 1 rain   // 6 cold water
                                         {8,8,2,2,7,5,5}, // 2 plain  // 7 sand 
                                         {2,2,1,1,7,5,5}, // 3 hill   // 8 Taiga
                                         {2,2,2,2,7,5,5}, // 3 hill   temperature increase as goes below
                                         {4,4,4,4,4,5,5}};// 4 desert sand humidity rises as goes right


    private void Awake()
    {
        Instance = this;
    }
    public void GenerateMap()
    {
        float[,] temperatureMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] humidityMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed + 1, noiseScale, octaves, persistance, lacunarity, offset);


        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.TileMap)
        {
            DrawTileMap(temperatureMap, humidityMap);
        }
        else if (drawMode == DrawMode.HumidityMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(humidityMap));
        }
        else if (drawMode == DrawMode.TemperatureMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(temperatureMap));
        }

        Debug.Log("Map Done");
    }

    private TileBase _tileBase;
    private Vector3Int _tilePos;
    private void DrawTileMap(float[,] tempMap, float[,] humidityMap)
    {
        ServiceProvider.GetDataManager.Initialize();
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentTemp = tempMap[x, y] * world.GetLength(0) == world.GetLength(0) ? world.GetLength(0) - 1 : tempMap[x, y] * world.GetLength(0);
                float currentHumidity = humidityMap[x, y] * world.GetLength(1) == world.GetLength(1) ? world.GetLength(1) - 1 : humidityMap[x, y] * world.GetLength(1);
                _tilePos = new Vector3Int(-(mapWidth / 2) + x, -(mapHeight / 2) + y);
                //_tileBase = regions[world[(int)currentTemp, (int)currentHumidity]].tileData.tiles[0];
                //tileMap.SetTile(_tilePos, _tileBase);
                TileData tileData = regions[world[(int)currentTemp, (int)currentHumidity]].tileData;
                tileMap.SetTile(_tilePos, tileData.tiles[0]);
                tileData.tilePositions.Add(_tilePos);
            }
        }
    }
    
    public void Flood()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (tileMap.GetTile(new Vector3Int(-(mapWidth / 2) + x, -(mapHeight / 2) + y)) == regions[7].tileData.tiles[0])
                {
                    tileMap.SetTile(new Vector3Int(-(mapWidth / 2) + x, -(mapHeight / 2) + y), regions[7].tileData.tiles[1]);
                }
            }
        }
    }

    public void DeleteTile()
    {
        tileMap.ClearAllTiles();
        ServiceProvider.GetDataManager.DeletePositions();
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public Color color;
    public TileData tileData;
}
