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
    private Player player;

    private void Awake()
    {
        _kickPlayerButton.onClick.AddListener(KickPlayer);
    }
    public void SetKickPlayerButtonVisible(bool visible)
    {
        _kickPlayerButton.gameObject.SetActive(visible);
    }
    public void UpdatePlayer(Player player)
    {
        this.player = player;
        _playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;
    }

    private void KickPlayer()
    {
        if (player != null)
        {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }
}
