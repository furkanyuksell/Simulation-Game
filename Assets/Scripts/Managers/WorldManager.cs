using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : NetworkBehaviour
{
    public static WorldManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // if game start different scenne then this will not work and return
        if (SceneManager.GetActiveScene().name != GameLoader.Scene.UIMenu.ToString())
        {
            MapGenerator.Instance.GenerateMap();
            return;   
        }
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
    }

    private void SceneManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        CreateMapClientRpc();
    }

    [ClientRpc]
    private void CreateMapClientRpc()
    {
        MapGenerator.Instance.GenerateMap();
    }

}
