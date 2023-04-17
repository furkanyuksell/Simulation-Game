using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyListUI : MonoBehaviour
{
    public static LobbyListUI Instance { get; private set; }

    [SerializeField] private Transform lobbySingleTemplate;
    [SerializeField] private Transform container;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LobbyManager.Instance.OnLobbyListChanged += LobbyManager_OnLobbyListChanged;
        lobbySingleTemplate.gameObject.SetActive(false);
    }
    private void LobbyManager_OnLobbyListChanged(object sender, LobbyManager.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }
    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach (Transform child in container)
        {
            if (child == lobbySingleTemplate) continue;

            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbySingleTransform = Instantiate(lobbySingleTemplate, container);
            lobbySingleTransform.gameObject.SetActive(true);
            LobbyListSingleUI lobbyListSingleUI = lobbySingleTransform.GetComponent<LobbyListSingleUI>();
            lobbyListSingleUI.UpdateLobby(lobby);
        }
    }

    void OnDestroy()
    {
        LobbyManager.Instance.OnLobbyListChanged -= LobbyManager_OnLobbyListChanged;
    }
}
