using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IProvidable
{
    [SerializeField] public GameObject MainUI;
    [SerializeField] public GameObject MultiPlayerUI;

    [SerializeField] public GameObject CreateLobbyUI;
    [SerializeField] public GameObject LobbyListUI;
    [SerializeField] public GameObject LobbyUI;

    private void Awake() {
        UIProvider.Register(this);
        MainUI.SetActive(true);
        LobbyUI.SetActive(true); // set start event after that its closed itself 
        LobbyListUI.SetActive(true); // set start event after that its closed itself 
    }

}
