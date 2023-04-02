using System;
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
    public static Action<Transform, string> OnJoiningLobby;

    private Lobby lobby;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            OnJoiningLobby?.Invoke(transform.parent, "Joining lobby...");
            LobbyManager.Instance.JoinWithId(lobby.Id);
        });
    }

    public void UpdateLobby(Lobby lobby)
    {
        this.lobby = lobby;

        _lobbyNameText.text = lobby.Name;
        _playersText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        _isPrivate.text = lobby.IsPrivate ? "Private" : "Public";
    }
}
