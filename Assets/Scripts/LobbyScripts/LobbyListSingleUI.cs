using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lobbyNameText;
    [SerializeField] private TextMeshProUGUI _playersText;
    [SerializeField] private TextMeshProUGUI _isPrivate;

    private Lobby lobby;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LobbyManager.Instance.JoinLobby(lobby);
        });
    }

    public void UpdateLobby(Lobby lobby)
    {
        this.lobby = lobby;

        _lobbyNameText.text = lobby.Name;
        _playersText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
    }
}
