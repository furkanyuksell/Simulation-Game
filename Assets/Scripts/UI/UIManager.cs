using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IProvidable
{
    [SerializeField] public GameObject _mainUI;
    [SerializeField] public GameObject _multiPlayerUI;

    [SerializeField] public GameObject _createLobbyUI;
    [SerializeField] public GameObject _lobbyListUI;
    [SerializeField] public GameObject _lobbyUI;

    private void Awake() {
        UIProvider.Register(this);
        _mainUI.SetActive(true);
        _lobbyUI.SetActive(true); // set start event after that its closed itself 
    }

}
