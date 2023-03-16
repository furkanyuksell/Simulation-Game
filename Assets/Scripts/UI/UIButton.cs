using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IProvidable
{
    [SerializeField] private Button _singleButton;
    [SerializeField] private Button _multiButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _joinLobbyButton;
    


    private void Awake() {
        UIProvider.Register(this);

        _singleButton.onClick.AddListener(() => {
            Debug.Log("Single Player");
        });

        _multiButton.onClick.AddListener(() => {
            UIProvider.GetUIManager.ShowMultiPlayerUI();
        });

        _settingsButton.onClick.AddListener(() => {
            Debug.Log("Settings");
        });

        _createLobbyButton.onClick.AddListener(() => {
            Debug.Log("Create Lobby");
        });
        
        _joinLobbyButton.onClick.AddListener(() => {
            Debug.Log("Join Lobby");
        });

    }    
}
