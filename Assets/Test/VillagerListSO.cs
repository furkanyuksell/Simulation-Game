using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class VillagerListSO : ScriptableObject
{
    public List<VillagerSO> villagerList;
}
