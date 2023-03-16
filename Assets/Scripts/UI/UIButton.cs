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
    [SerializeField] private Button _findLobbyButton;
    
    [SerializeField] private Button _statusButton;
    bool isPrivateLobby = false;

    [SerializeField] private Button _createLobbyButton;


    private void Awake() {
        _singleButton.onClick.AddListener(() => {
            Debug.Log("Single Player");
        });

        _multiButton.onClick.AddListener(() => {
            UIProvider.GetUIManager.ShowMultiPlayerUI();
        });

        _settingsButton.onClick.AddListener(() => {
            Debug.Log("Settings");
        });

        _showCreateLobbyUI.onClick.AddListener(() => {
            UIProvider.GetUIManager.ShowCreateLobbyUI();
            LobbyManager.Instance.Authenticate(UIProvider.GetUITextbox._playerName.text);
        });

        _findLobbyButton.onClick.AddListener(() => {
            LobbyManager.Instance.Authenticate(UIProvider.GetUITextbox._playerName.text);
        });


        _statusButton.onClick.AddListener(() => {
            Debug.Log(UIProvider.GetUITextbox._lobbyStatus.text);
            if(UIProvider.GetUITextbox._lobbyStatus.text.Equals("Private"))
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

        _createLobbyButton.onClick.AddListener(() => {
            LobbyManager.Instance.CreateLobby(UIProvider.GetUITextbox._lobbyName.text, int.Parse(UIProvider.GetUITextbox._lobbyPlayerCount.text), isPrivateLobby);
        });
    }    
}
