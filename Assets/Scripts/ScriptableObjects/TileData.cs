using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public float hungerRate;
    public float thirstRate;
    public float chillRate;
    public TileBase[] tiles;
}
