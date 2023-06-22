using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VillagerSO : ScriptableObject
{ 
    public string villagerName;
    public string job;
    public string task;
    public Sprite sprite;
    public VillagerBase prefab;
    public List<InputController.SelectableTypes> selectableTypes = new();
}
