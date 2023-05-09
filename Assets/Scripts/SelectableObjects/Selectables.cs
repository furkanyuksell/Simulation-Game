using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectables : MonoBehaviour
{
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
    }
}
