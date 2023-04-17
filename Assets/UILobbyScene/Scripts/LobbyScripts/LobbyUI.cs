using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Relay;
using Unity.Netcode;
using System;

public class LobbyUI : MonoBehaviour
{
    public static LobbyUI Instance { get; private set; }
    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject playAgainContainer;
    [SerializeField] private Button playAgainButton;
    private void Awake()
    {
        Instance = this;
    }
 
    private void LobbyUI_GetActive(object sender, System.EventArgs e)
    {
        Debug.Log("LOBBYUI START");
        Lobby lobby = LobbyManager.Instance.GetLobby();
        lobbyNameText.text = lobby.Name;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_Server_OnClientDisconnectCallback;
        NetworkConnection.Instance.OnPlayerDataNetworkListChanged += UpdateLobby_Event;

        leaveLobbyButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.LeaveLobby();
            NetworkManager.Singleton.Shutdown();
            GameReset.Instance.ResetAllData();
            GameLoader.Load(GameLoader.Scene.UIMenu);
        });

        startGameButton.onClick.AddListener(() =>
        {
            Debug.Log("StartGameCLicked");
            NetworkGameStarter.Instance.StartGame();
        });

        playerSingleTemplate.gameObject.SetActive(false);
        NetworkConnection.Instance.RefreshLobbyPlayersUI();
    }
    
    private void LobbyManager_OnDestroyLobby(object sender, System.EventArgs e)
    {
        ClearLobby();
    }

    private void UpdateLobby_Event(object sender, System.EventArgs e)
    {
        Debug.Log("AMA NOT Updating lobby");
        UpdateLobby();
    }

    private void UpdateLobby()
    {
        Debug.Log("Updating lobby");
        ClearLobby();

        if (LobbyManager.Instance.IsLobbyHost())
        {
            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }

        NetworkList<PlayerData> playerDataList = NetworkConnection.Instance.GetPlayerDataNetworkList();
        foreach (PlayerData playerData in playerDataList)
        {
            Debug.Log("ClientId: " + playerData.clientId + " PlayerId: " + playerData.playerId + " PlayerName: " + playerData.playerName);
            Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
            LobbyPlayerSingleUI lobbyPlayerSingleUI = playerSingleTransform.GetComponent<LobbyPlayerSingleUI>();
            lobbyPlayerSingleUI.gameObject.SetActive(true);
            lobbyPlayerSingleUI.SetKickPlayerButtonVisible(
                LobbyManager.Instance.IsLobbyHost() &&
                playerData.playerId != AuthenticationService.Instance.PlayerId // Don't allow kick self
            );
            lobbyPlayerSingleUI.UpdatePlayer(playerData);
        }
    }
    private void ClearLobby()
    {
        foreach (Transform child in container)
        {
            if (child == playerSingleTemplate) continue;
            Destroy(child.gameObject);
        }
    }
    private void NetworkManager_Server_OnClientDisconnectCallback(ulong clientId)
    {
        if(clientId == NetworkManager.ServerClientId)
        {
            playAgainContainer.gameObject.SetActive(true);
            playAgainButton.onClick.AddListener(() =>
            {
                GameReset.Instance.ResetAllData();
                GameLoader.Load(GameLoader.Scene.UIMenu);
            });
        }   
    }


    void OnEnable()
    {
        LobbyManager.Instance.OnCreateLobby += LobbyUI_GetActive;
        LobbyManager.Instance.OnJoinedLobby += LobbyUI_GetActive;
    }

    void OnDestroy()
    {
        NetworkConnection.Instance.OnPlayerDataNetworkListChanged -= UpdateLobby_Event;
        LobbyManager.Instance.OnCreateLobby -= LobbyUI_GetActive;
        LobbyManager.Instance.OnJoinedLobby -= LobbyUI_GetActive;
    }
}
