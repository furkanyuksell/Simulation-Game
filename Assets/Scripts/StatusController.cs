using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    private float hungerResistance;
    private float thirstResistance;
    public float chillResistance;
    public float depressionResistance;

    private float hungerStatus;
    private float thirstStatus;
    private float chillStatus;
    private float depressionStatus;

    private float timeWithoutDoingAnything = 0f;

    public void OneCyclePass(TileData regionData)
    {
        hungerStatus += regionData.hungerRate / hungerResistance;
        thirstStatus += regionData.thirstRate / thirstResistance;
        chillStatus += regionData.chillRate / chillResistance;
        timeWithoutDoingAnything++;
        if(timeWithoutDoingAnything > depressionResistance)
        {
            // Do Bad Things
        }
    } 
}
