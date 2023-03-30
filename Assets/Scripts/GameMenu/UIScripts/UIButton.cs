using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    [SerializeField] private Button _saveNameButton;

    [SerializeField] private Button _backButton;
    Stack<GameObject> _backQueue = new Stack<GameObject>();
    bool isPrivateLobby = false;
    bool isSignedIn = false; // i'll fix with this later on for the sign once  

    private void Start()
    {
        _backQueue.Push(UIProvider.GetUIManager.MainUI);
        LobbyManager.Instance.OnJoinedLobby += LobbyManager_JoinedLobby;
        LobbyManager.Instance.OnLeftLobby += LobbyManager_LeftLobby;
        LobbyManager.Instance.OnDestroyLobby += LobbyManager_LeftLobby;
        LobbyManager.Instance.OnKickedFromLobby += LobbyManager_LeftLobby;

        _singleButton.onClick.AddListener(() =>
        {
            Debug.Log("Single Player");
        });

        _multiButton.onClick.AddListener(() =>
        {
            MoveForwardInStack(UIProvider.GetUIManager.MultiPlayerUI);
            _backButton.gameObject.SetActive(true);
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
                LobbyManager.Instance.Authenticate(UIProvider.GetUITextbox.PlayerName.text);
            MoveForwardInStack(UIProvider.GetUIManager.LobbyListUI);
            isSignedIn = true;
            if (!_saveNameButton.IsActive())
                _saveNameButton.gameObject.SetActive(true);
        });

        _backButton.onClick.AddListener(() =>
        {
            BackwardInStack();
        });

        _statusButton.onClick.AddListener(() =>
        {
            Debug.Log(UIProvider.GetUITextbox.LobbyStatus.text);
            if (UIProvider.GetUITextbox.LobbyStatus.text.Equals("Private"))
            {
                UIProvider.GetUITextbox.LobbyStatus.text = "Public";
                isPrivateLobby = false;
            }
            else
            {
                UIProvider.GetUITextbox.LobbyStatus.text = "Private";
                isPrivateLobby = true;
            }
        });

        _createLobbyButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.CreateLobby(UIProvider.GetUITextbox.LobbyName.text, int.Parse(UIProvider.GetUITextbox.LobbyPlayerCount.text), isPrivateLobby);            
        });

        _saveNameButton.onClick.AddListener(() =>
        {
            LobbyManager.Instance.UpdatePlayerName(UIProvider.GetUITextbox.PlayerName.text);
        });
        _saveNameButton.gameObject.SetActive(false);
    }

    private void LobbyManager_JoinedLobby(object sender, System.EventArgs e)
    {
        _backButton.gameObject.SetActive(false);
        MoveForwardInStack(UIProvider.GetUIManager.MultiPlayerUI, UIProvider.GetUIManager.LobbyUI);
    }
    private void LobbyManager_LeftLobby(object sender, System.EventArgs e)
    {
        _backButton.gameObject.SetActive(true);
        MoveForwardInStack(UIProvider.GetUIManager.MultiPlayerUI, UIProvider.GetUIManager.LobbyListUI);
    }

    private void MoveForwardInStack(GameObject gameObject)
    {
        _backQueue.Peek().SetActive(false);
        _backQueue.Push(gameObject);
        _backQueue.Peek().SetActive(true);
    }

    private void MoveForwardInStack(GameObject backwardPos, GameObject forwardPos)
    {
        while (_backQueue.Peek() != backwardPos)
        {
            _backQueue.Pop().SetActive(false);
        }
        _backQueue.Push(forwardPos);
        _backQueue.Peek().SetActive(true);
    }

    private void BackwardInStack()
    {
        if (_backQueue.Count > 1)
        {
            _backQueue.Pop().SetActive(false);
            _backQueue.Peek().SetActive(true);
            if (_backQueue.Count == 1)
                _backButton.gameObject.SetActive(false);
        }
    }

    private void ShowCreateLobbyUI()
    {
        if (_backQueue.Peek() != UIProvider.GetUIManager.CreateLobbyUI)
            MoveForwardInStack(UIProvider.GetUIManager.CreateLobbyUI);
        if (!isSignedIn)
            LobbyManager.Instance.Authenticate(UIProvider.GetUITextbox.PlayerName.text);
        isSignedIn = true;

        if (!_saveNameButton.IsActive())
            _saveNameButton.gameObject.SetActive(true);
    }
}