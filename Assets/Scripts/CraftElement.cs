using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class CraftElement : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    private GameObject _craftElement;
    private bool _isPlacing;
    private Camera _camera;
    private Vector3 _offset = new Vector3(0, 0, 10);
    public Func<int, int, bool> CraftableSelected;
    private BoxCollider2D _col;

    [SerializeField] private TMP_Text woodCostTXT;
    [SerializeField] private TMP_Text stoneCostTXT;
    private int woodCost;
    private int stoneCost;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void SetData(GameObject craftElement, string craftName, int woodCostVal, int stoneCostVal)
    {
        _craftElement = craftElement;
        buttonText.text = craftName;
        woodCost = woodCostVal;
        stoneCost = stoneCostVal;
        woodCostTXT.text = woodCost.ToString();
        stoneCostTXT.text = stoneCost.ToString();
    }

    public void OnClick()
    {
        if ((bool)CraftableSelected?.Invoke(woodCost, stoneCost))
        {
            GameObject go = Instantiate(_craftElement, _camera.ScreenToWorldPoint(Input.mousePosition) + _offset, Quaternion.identity);
            _col = go.GetComponent<BoxCollider2D>();
            _col.enabled = false;
            _isPlacing = true;
            StartCoroutine(PlaceObject(go));
        }
    }

    IEnumerator PlaceObject(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        while (_isPlacing)
        {
            go.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition) + _offset;
            if (Input.GetMouseButtonDown(0))
            {
                if (!Physics2D.OverlapBox((Vector2)_camera.ScreenToWorldPoint(Input.mousePosition), _col.size, 0))
                {
                    _col.enabled = true;
                    _isPlacing = false;
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Destroy(go);
                _isPlacing = false;
            }
            yield return Time.deltaTime;
        }
        yield return null; ;
    }
}
