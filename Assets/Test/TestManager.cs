using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : NetworkBehaviour
{
    public static TestManager Instance;
    [SerializeField] public TextMeshProUGUI testText;
    private void Awake()
    {
        Instance = this;
        // if game start different scenne then this will not work and return
        if (SceneManager.GetActiveScene().name != GameLoader.Scene.UIMenu.ToString())
            return;
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
    }
    private void SceneManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        testText.text = sceneName;
        CreateMapClientRpc();
    }

    [ClientRpc]
    private void CreateMapClientRpc()
    {
        MapGenerator.Instance.GenerateMap();
    }

}
