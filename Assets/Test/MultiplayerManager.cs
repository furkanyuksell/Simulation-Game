using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerManager : NetworkBehaviour
{
    public static MultiplayerManager Instance;
    public static Action OnServerDataLoad;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void InitGameFromServer()
    {
        if (!IsServer)
            return;
        OnServerDataLoad?.Invoke();
    }

    private VillagerBase _villagerBase;
    [ServerRpc (RequireOwnership = false)]
    private void SpawnRequestVillagerServerRpc(int villagerIndex, ulong clientId)
    {
        VillagerSO villagerSo = GetVillagerFromIndex(villagerIndex);
        VillagerBase villagerBase = Instantiate(villagerSo.prefab);
        NetworkObject networkObject = villagerBase.GetComponent<NetworkObject>();
        networkObject.Spawn();
        networkObject.ChangeOwnership(clientId);
        villagerBase.SetVillagerToManagerClientRpc(villagerIndex);
    }
   
    
    public void TellWannaSpawnToServer(VillagerSO villager, ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("not local client");
            return;
        }
        
        SpawnRequestVillagerServerRpc(GetVillagerIndex(villager), clientId);
    }

    private int GetVillagerIndex(VillagerSO villager)
    {
        var indexOfVillager = ServiceProvider.GetDataManager.VillagerListSO.villagerList.IndexOf(villager);
        return indexOfVillager;
    }

    private VillagerSO GetVillagerFromIndex(int index)
    {
        var villager = ServiceProvider.GetDataManager.VillagerListSO.villagerList[index];
        return villager;
    }

    private void OnEnable()
    {
        MapGenerator.OnMapDone += InitGameFromServer;
    }

    private void OnDisable()
    {
        MapGenerator.OnMapDone -= InitGameFromServer;
    }
}
