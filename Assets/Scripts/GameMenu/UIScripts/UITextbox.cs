using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextbox : MonoBehaviour, IProvidable
{
    [SerializeField] public TMP_InputField PlayerName;
    [SerializeField] public TMP_InputField LobbyName;
    [SerializeField] public TMP_InputField LobbyPlayerCount;
    [SerializeField] public TextMeshProUGUI LobbyStatus;
    private void Awake()
    {
        UIProvider.Register(this);
    }

    void Start()
    {
        PlayerName.text = NetworkConnection.Instance.GetPlayerName();
        PlayerName.onValueChanged.AddListener((string value) =>
        {
            NetworkConnection.Instance.SetPlayerName(value);
        });
    }
}
