using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleCharacterUIPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _jobText;
    [SerializeField] private TextMeshProUGUI _taskText;


    public void SetCharacterInfo(string characterName, string job, string task)
    {
        _nameText.text = characterName;
        _jobText.text = job;
        _taskText.text = task;
    }
}
