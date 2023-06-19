using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public static VillagerManager Instance { get; set; }
    public List<VillagerBase> villagerList = new List<VillagerBase>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void AddVillager(VillagerBase villager, ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId)
            return; 
        villagerList.Add(villager);
    }

    private void InitVillagers()
    {
        foreach (var villagerSo in ServiceProvider.GetDataManager.VillagerListSO.villagerList)
        {
            MultiplayerManager.Instance.TellWannaSpawnToServer(villagerSo, NetworkManager.Singleton.LocalClientId);
        }
    }

    private void OnEnable()
    {
        LoaderUI.OnLoaderUIFinished += InitVillagers;
    }

    private void OnDestroy()
    {
        LoaderUI.OnLoaderUIFinished -= InitVillagers;    
    }
}

