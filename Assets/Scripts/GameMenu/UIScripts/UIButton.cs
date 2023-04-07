using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private Button _backButton;
    Stack<GameObject> _backQueue = new Stack<GameObject>();
    bool isPrivateLobby = false;
    private GameObject _warningBox;
    private void Start()
    {
        _backQueue.Push(UIProvider.GetUIManager.MainUI);
        LobbyManager.Instance.OnCreateLobby += LobbyManager_JoinedLobby;
        LobbyManager.Instance.OnJoinedLobby += LobbyManager_JoinedLobby;
        LobbyManager.Instance.OnLeftLobby += LobbyManager_LeftLobby;
        LobbyListSingleUI.OnJoiningLobby += WarningBoxInfos;
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
            MoveForwardInStack(UIProvider.GetUIManager.LobbyListUI);
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
            WarningBoxInfos(UIProvider.GetUIManager.CreateLobbyUI.transform, "Creating Lobby...");
            LobbyManager.Instance.CreateLobby(UIProvider.GetUITextbox.LobbyName.text, isPrivateLobby);
        });
    }

    private void WarningBoxInfos(Transform parent, string info)
    {
        _warningBox = Instantiate(UIProvider.GetUIManager.WarningBox, parent);
        _warningBox.GetComponentInChildren<TextMeshProUGUI>().text = info;
    }

    private void LobbyManager_JoinedLobby(object sender, System.EventArgs e)
    {
        Destroy(_warningBox);
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
    }
    void OnDisable()
    {
        LobbyListSingleUI.OnJoiningLobby -= WarningBoxInfos;
    }
}
