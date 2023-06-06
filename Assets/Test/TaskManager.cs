using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private readonly HashSet<Selectables> _taskHashSet = new HashSet<Selectables>();
    
    private void Awake()
    {
        Instance = this;
    }

    public void OpenNewTask(Selectables task)
    {
        _taskHashSet.Add(task);
    }
    
    public void InitTask()
    {
        List<Selectables> taskList = _taskHashSet.ToList();
        bool isTaskAssigned = VillagerManager.Instance.SetTaskToVillager(taskList);
        if (isTaskAssigned)
        {
            Debug.Log("Task Assigned");
            _taskHashSet.Clear();
        }
    }

}
