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
