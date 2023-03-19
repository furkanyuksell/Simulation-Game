using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap,
        ColorMap,
        TileMap
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
    private int[,] world = new int[5,6]{ {0,0,0,0,3,6}, // 0 snow, 1 rain forest, 2 plain, 3 rock, 4 desert, 5 water,  6 cold water, 7 sand
                                         {3,3,1,1,1,6}, // temperature increase as goes below, humidity rises as goes right
                                         {3,2,2,1,1,5},
                                         {2,2,2,2,2,5},
                                         {4,4,4,4,7,5} }; 

    public void GenerateMap()
    {
        float[,] temperatureMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] humidityMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed + 1, noiseScale, octaves, persistance, lacunarity, offset);


        //Color[] colorMap = new Color[mapHeight * mapWidth];
        //for (int y = 0; y < mapHeight; y++)
        //{
        //    for (int x = 0; x < mapWidth; x++)
        //    {
        //        float currentHeight = temperatureMap[x, y];
        //        for (int i = 0; i < regions.Length; i++)
        //        {
        //            if (currentHeight <= regions[i].height)
        //            {
        //                colorMap[y * mapWidth + x] = regions[i].color;
        //                break;
        //            }
        //        }
        //    }
        //}


        //MapDisplay display = FindObjectOfType<MapDisplay>();
        //if (drawMode == DrawMode.NoiseMap)
        //{
        //    display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        //}
        //else if (drawMode == DrawMode.ColorMap)
        //{
        //    display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        //}
        //else if (drawMode == DrawMode.TileMap)
        //{
        DrawTileMap(temperatureMap, humidityMap);
        //}

    }

    private void DrawTileMap(float[,] tempMap, float[,] humidityMap)
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentTemp = tempMap[x, y] * 5 == 5 ? 4 : tempMap[x, y] * 5;
                float currentHumidity = humidityMap[x, y] * 6 == 6 ? 5 : humidityMap[x, y] * 6;
                tileMap.SetTile(new Vector3Int(-(mapWidth / 2) + x, -(mapHeight / 2) + y), regions[world[(int)currentTemp,(int)currentHumidity]].tileData.tiles[0]);
            }
        }
    }

    //private void DrawTileMap(float[,] tempMap, float[,] humidityMap)
    //{
    //    for (int y = 0; y < mapHeight; y++)
    //    {
    //        for (int x = 0; x < mapWidth; x++)
    //        {
    //            float currentTemp = tempMap[x, y];
    //            float currentHumidity = humidityMap[x, y];
    //            for (int i = 0; i < regions.Length; i++)
    //            {
    //                if (currentTemp <= regions[i].temperature && currentHumidity <= regions[i].humidity)
    //                {
    //                    tileMap.SetTile(new Vector3Int(-(mapWidth / 2) + x, -(mapHeight / 2) + y), regions[i].tileData.tiles[0]);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}
//
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
