using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextbox : MonoBehaviour, IProvidable
{
    [SerializeField] public TMP_InputField _playerName;
    [SerializeField] public TMP_InputField _lobbyName;
    [SerializeField] public TMP_InputField _lobbyPlayerCount;
    [SerializeField] public TextMeshProUGUI _lobbyStatus;
    private void Awake()
    {
        UIProvider.Register(this);
    }
}
