using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Button _singleButton;
    [SerializeField] private Button _multiButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _showCreateLobbyUI;
    [SerializeField] private Button _showCreateLobbyUIFromLobbyList;
    [SerializeField] private Button _findLobbyButton;
    [SerializeField] private Button _statusButton;
    [SerializeField] private Button _createLobbyButton;

    bool isPrivateLobby = false;
    bool isSignedIn = false; // i'll fix with this later on for the sign once  


    private void Awake()
    {
        _singleButton.onClick.AddListener(() =>
        {
            Debug.Log("Single Player");
        });

        _multiButton.onClick.AddListener(() =>
        {
            UIProvider.GetUIManager.ShowMultiPlayerUI();
        });

        _settingsButton.onClick.AddListener(() =>
        {
            Debug.Log("Settings");
        });

        _showCreateLobbyUI.onClick.AddListener(() =>
        {
            ShowCreateLobbyUI();
        });

        _showCreateLobbyUIFromLobbyList.onClick.AddListener(() =>
        {
            ShowCreateLobbyUI();
        });

        _findLobbyButton.onClick.AddListener(() =>
        {
            if (!isSignedIn)
                LobbyManager.Instance.Authenticate(UIProvider.GetUITextbox._playerName.text);
            UIProvider.GetUIManager.ShowLobbyList();
            isSignedIn = true;

        });


        _statusButton.onClick.AddListener(() =>
        {
            Debug.Log(UIProvider.GetUITextbox._lobbyStatus.text);
            if (UIProvider.GetUITextbox._lobbyStatus.text.Equals("Private"))
            {
                UIProvider.GetUITextbox._lobbyStatus.text = "Public";
                isPrivateLobby = false;
            }
            else
            {
                UIProvider.GetUITextbox._lobbyStatus.text = "Private";
                isPrivateLobby = true;
            }
        });

        _createLobbyButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.CreateLobby(UIProvider.GetUITextbox._lobbyName.text, int.Parse(UIProvider.GetUITextbox._lobbyPlayerCount.text), isPrivateLobby);
        });
    }

    private void ShowCreateLobbyUI()
    {
        if (!isSignedIn)
            LobbyManager.Instance.Authenticate(UIProvider.GetUITextbox._playerName.text);
        UIProvider.GetUIManager.ShowCreateLobbyUI();
        isSignedIn = true;
    }
}
