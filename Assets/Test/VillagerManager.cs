using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class VillagerManager : NetworkBehaviour
{
    public static VillagerManager Instance { get; set; }
    public List<VillagerBase> villagerList = new List<VillagerBase>();
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void AddVillager(VillagerBase villager)
    {
        villagerList.Add(villager);
    }

    public bool SetTaskToVillager(List<Selectables> task)
    {
        if (!IsOwner)
        {
            return false;
        }
        foreach (var villagers in villagerList)
        {
            if (!villagers.hasTask)
            {
                if (villagers.selectableTypes.Contains(task[0].selectableTypes))
                {
                    villagers.StartTask(task);
                    return true;
                }
            }
        }

        return false;
    }

}

