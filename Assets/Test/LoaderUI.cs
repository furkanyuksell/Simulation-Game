using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderUI : MonoBehaviour
{
    public static Action OnLoaderUIFinished;
    [SerializeField] Slider _slider;
    void Start()
    {
        StartCoroutine(SliderCoroutine());
    }

    IEnumerator SliderCoroutine()
    {
        while (_slider.value < 1)
        {
            _slider.value += 0.1f;
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("SliderCoroutine finished");
        OnLoaderUIFinished?.Invoke();
        gameObject.SetActive(false);
    }
}
