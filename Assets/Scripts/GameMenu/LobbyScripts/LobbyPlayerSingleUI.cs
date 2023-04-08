using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private TextMeshProUGUI _playerAddInfo; // if its needed
    [SerializeField] private Button _kickPlayerButton;
    private PlayerData playerData;

    private void Awake()
    {
        _kickPlayerButton.onClick.AddListener(() =>
        {
            Debug.Log("Kicking player " + playerData.playerName + " with id " + playerData.playerId);
            LobbyManager.Instance.KickPlayer(playerData.playerId.ToString());
            NetworkConnection.Instance.KickPlayer(playerData.clientId);
        });
    }
    public void SetKickPlayerButtonVisible(bool visible)
    {
        _kickPlayerButton.gameObject.SetActive(visible);
    }
    public void UpdatePlayer(PlayerData playerData)
    {
        this.playerData = playerData;
        _playerNameText.text = playerData.playerName.ToString();
    }

}
