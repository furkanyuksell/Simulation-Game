using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkServerManager : NetworkBehaviour
{
    public static Action OnServerDataLoad;
    void Start()
    {
    }

    private void InitGameFromServer()
    {
        if (!IsServer)
            return;
        OnServerDataLoad?.Invoke();
    }

    private void OnEnable()
    {
        MapGenerator.OnMapDone += InitGameFromServer;
    }

    private void OnDisable()
    {
        MapGenerator.OnMapDone -= InitGameFromServer;
    }
}
