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
        Debug.Log("SpawnRequestVillagerServerRpc");
        VillagerSO villagerSo = GetVillagerFromIndex(villagerIndex);
        VillagerBase villagerBase = Instantiate(villagerSo.prefab);
        NetworkObject networkObject = villagerBase.GetComponent<NetworkObject>();
        networkObject.Spawn();
        networkObject.ChangeOwnership(clientId);
        villagerBase.SetVillagerToManagerClientRpc();
        //villagerBase.PlayerNameText.text = NetworkConnection.Instance.GetPlayerDataFromClientId(clientId).playerName.ToString();
    }
   
    
    public void TellWannaSpawnToServer(VillagerSO villager, ulong clientId)
    {
        Debug.Log("tellWannaSpawnToServer");
        SpawnRequestVillagerServerRpc(GetVillagerIndex(villager), clientId);
    }

    private int GetVillagerIndex(VillagerSO villager)
    {
        var indexOfVillger = ServiceProvider.GetDataManager.VillagerListSO.villagerList.IndexOf(villager);
        return indexOfVillger;
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
