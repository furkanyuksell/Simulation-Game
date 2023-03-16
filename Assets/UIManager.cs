using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IProvidable
{
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _multiPlayerUI;


    private void Awake() {
        UIProvider.Register(this);
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
}
