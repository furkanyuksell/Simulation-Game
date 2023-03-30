using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkConnection : PersistentSingleton<NetworkConnection>
{
    public void HostConnection()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void ClientConnection()
    {
        NetworkManager.Singleton.StartClient();
    }

}
