using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilServices
{
    public static int GetRandomNumber(int inclusive, int exclusive)
    {
        return Random.Range(inclusive, exclusive);
    }
}
