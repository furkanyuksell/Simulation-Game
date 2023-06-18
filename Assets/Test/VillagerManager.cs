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
    
    public void AddVillager(VillagerBase villager, ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId)
            return; 
        villagerList.Add(villager);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MultiplayerManager.Instance.TellWannaSpawnToServer(ServiceProvider.GetDataManager.VillagerListSO.villagerList[0], NetworkManager.Singleton.LocalClientId);
        }
    }
}

