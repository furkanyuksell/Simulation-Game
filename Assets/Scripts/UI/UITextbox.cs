using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextbox : MonoBehaviour, IProvidable
{
    [SerializeField] public TextMeshProUGUI _playerName;
    [SerializeField] public TextMeshProUGUI _lobbyName;
    [SerializeField] public TextMeshProUGUI _lobbyPlayerCount;
    [SerializeField] public TMP_InputField _lobbyNameInputField;
    private void Awake()
    {
        UIProvider.Register(this);
    }
}
