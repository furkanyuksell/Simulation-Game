using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleCharacterUIPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _jobText;
    [SerializeField] private TextMeshProUGUI _taskText;
    [SerializeField] private Image _image;

    public void SetCharacterInfo(string characterName, string job, string task, Sprite sprite)
    {
        _nameText.text = characterName;
        _jobText.text = job;
        _taskText.text = task;
        _image.sprite = sprite;
    }
}
