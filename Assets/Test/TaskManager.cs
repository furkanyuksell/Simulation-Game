using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private readonly HashSet<Selectables> _taskList = new();
    
    private void Awake()
    {
        Instance = this;
    }

    public void OpenNewTask(Selectables task)
    {
        if (_taskList.Add(task))
        {
            
        }
    }
    
    
    public bool SetTaskToVillager(List<Selectables> task)
    {
        foreach (var villagers in VillagerManager.Instance.villagerList)
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
    /*
    public void InitTask()
    {
        List<Selectables> taskList = _taskList.ToList();
        bool isTaskAssigned = VillagerManager.Instance.SetTaskToVillager(taskList);
        if (isTaskAssigned)
        {
            _taskList.Clear();
        }
    }
    */
}
