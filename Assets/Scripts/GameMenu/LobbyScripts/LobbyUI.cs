using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Relay;
using Unity.Netcode;

public class LobbyUI : MonoBehaviour
{
    public static LobbyUI Instance { get; private set; }

    [SerializeField] private Transform playerSingleTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private Button leaveLobbyButton;
    [SerializeField] private Button startGameButton;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    private void LobbyUI_GetActive(object sender, System.EventArgs e)
    {
        Debug.Log("LOBBYUI START");
        Lobby lobby = LobbyManager.Instance.GetLobby();
        lobbyNameText.text = lobby.Name;
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
            Debug.Log("Starting game");
            LobbyManager.Instance.DeleteLobby();
            GameLoader.LoadNetworkGame("WorldScene");
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
            Debug.Log("Connected Client Id: " + playerData.clientId);
            Debug.Log("Connected Client Id: " + playerData.playerName);
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
