using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

public class TestWorldManager : NetworkBehaviour
{
    [SerializeField] private Transform playerPrefab;
    public override void OnNetworkSpawn()
    {
        Debug.Log("OnNetworkSpawn");
        if (IsServer)
        {
            Debug.Log("Server");
            NetworkManager.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;

        }
    }
    private void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
        {
            foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                Debug.Log("Spawning player for client: " + clientId);
                Transform playerTransform = Instantiate(playerPrefab);
                playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
            }
        }
    }
}
