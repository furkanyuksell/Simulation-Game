using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectables : MonoBehaviour
{
    public int woodCount, stoneCount, foodCount, herbCount;
    public InputController.SelectableTypes selectableTypes;
    private GameObject _selectedGO;
    private void Awake()
    {
        _selectedGO = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible)
    {
        _selectedGO.SetActive(visible);
        if (visible)
        {
            TaskManager.Instance.OpenNewTask(this);
        }
    }

}
