using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameReset : MonoBehaviour
{
    public static GameReset Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ResetAllData()
    {
        UIProvider.ResetDictionary();
        ServiceProvider.ResetDictionary(); // ??????? InGame Func

        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }

        if (LobbyManager.Instance != null)
        {
            Destroy(LobbyManager.Instance.gameObject);
        }

        if (NetworkConnection.Instance != null)
        {
            Destroy(NetworkConnection.Instance.gameObject);
        }
    }
}
