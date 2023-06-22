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
    
    
    public void SetTaskToVillager()
    {
        if (_taskList.Count != 0)
        {
            List<Selectables> taskList = _taskList.ToList();
            foreach (var villagers in VillagerManager.Instance.villagerList)
            {
                if (!villagers.hasTask)
                {
                    if (villagers.selectableTypes.Contains(taskList[0].selectableTypes))
                    {
                        villagers.StartTask(_taskList.ToList());
                    }
                }
            }
            _taskList.Clear();
        }
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
