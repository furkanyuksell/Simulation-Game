using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CraftElement : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    private GameObject _craftElement;
    private bool _isPlacing;
    private Camera _camera;
    private Vector3 _offset = new Vector3(0,0,10);
    public UnityAction CraftableSelected;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void SetData(GameObject craftElement, string craftName)
    {
        _craftElement = craftElement;
        buttonText.text = craftName;
    }

    public void OnClick()
    {
        CraftableSelected?.Invoke();
        GameObject go = Instantiate(_craftElement,_camera.ScreenToWorldPoint(Input.mousePosition) + _offset,Quaternion.identity);
        _isPlacing = true;
        StartCoroutine(PlaceObject(go));
    }

    IEnumerator PlaceObject(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        while(_isPlacing)
        {
            go.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition) + _offset;
            if(Input.GetMouseButtonDown(0))
            {
                _isPlacing = false;
            }
            yield return Time.deltaTime;
        }
        yield return null;;
    }
}
