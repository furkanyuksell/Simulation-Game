using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CraftElement : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    private UnityAction OnCreateCraft;

    public void SetData(UnityAction onClickedButton, string craftName)
    {
        OnCreateCraft = onClickedButton;
        buttonText.text = craftName;
    }

    public void OnClick()
    {
        OnCreateCraft?.Invoke();
    }
}
