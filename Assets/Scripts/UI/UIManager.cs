using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IProvidable
{
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _multiPlayerUI;

    [SerializeField] private GameObject _createLobbyUI;

    private void Awake() {
        UIProvider.Register(this);
        _mainUI.SetActive(true);
    }

    public void ShowMainUI()
    {
        _mainUI.SetActive(true);
        _multiPlayerUI.SetActive(false);
    }

    public void ShowMultiPlayerUI()
    {
        _mainUI.SetActive(false);
        _multiPlayerUI.SetActive(true);
    }

    public void ShowCreateLobbyUI()
    {
        _multiPlayerUI.SetActive(false);
        _createLobbyUI.SetActive(true);
    }
}
