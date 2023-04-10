using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkGameStarter : NetworkBehaviour
{
    public static NetworkGameStarter Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        StartGameFromServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void StartGameFromServerRpc(ServerRpcParams serverRpcParams = default)
    {
        LobbyManager.Instance.DeleteLobby();
        GameLoader.LoadNetworkGame("WorldScene");
    }
}
