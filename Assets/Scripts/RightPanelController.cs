using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPanelController : MonoBehaviour
{
    private RectTransform _panel;
    public float slideSpeed = 0.3f;
    private float _targetX; 
    private bool _isPanelOpen;

    public RightPanelController()
    {
        _isPanelOpen = false;
    }

    private void Awake()
    {
        _panel = GetComponent<RectTransform>();
    }
    public void MovePanelToLeft()
    {
        if (!_isPanelOpen)
        {
            _targetX = -500f;
            StartCoroutine(SlideAnimation());   
        }
        else
        {
            _targetX = 0f;
            StartCoroutine(SlideAnimation());
        }
    }

    private IEnumerator SlideAnimation()
    {

        float startX = _panel.anchoredPosition.x;
        float elapsedTime = 0f;

        while (elapsedTime < slideSpeed)
        {
            float newX = Mathf.Lerp(startX, _targetX, elapsedTime / slideSpeed);
            _panel.anchoredPosition = new Vector2(newX, _panel.anchoredPosition.y);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _panel.anchoredPosition = new Vector2(_targetX, _panel.anchoredPosition.y);
        _isPanelOpen = !_isPanelOpen;
    }
}
