using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragSelectionController : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;
    private InputController.SelectableTypes currentSelection; 
    private List<Selectables> _selectedGOList = new List<Selectables>();
    [SerializeField] private Transform _selectionAreaTransform;

    private void Awake()
    {
        _selectionAreaTransform.gameObject.SetActive(false);
    }


    public void SelectionStarted(Vector3 startPos,InputController.SelectableTypes type)
    {
        _startPos = startPos;
        _selectionAreaTransform.gameObject.SetActive(true);
        SetSelectionPosition(startPos);
        currentSelection = type;
    }

    public void SetSelectionPosition(Vector3 currentPos)
    {   
        Vector3 lowerLeft = new Vector3(Mathf.Min(_startPos.x,currentPos.x),
                                        Mathf.Min(_startPos.y,currentPos.y));
        Vector3 upperRight = new Vector3(Mathf.Max(_startPos.x,currentPos.x),
                                        Mathf.Max(_startPos.y,currentPos.y));
        
        _selectionAreaTransform.position = lowerLeft;
        _selectionAreaTransform.localScale = upperRight - lowerLeft;
    }

    public List<Selectables> SelectionEnded(Vector3 endPos)
    {
        _selectionAreaTransform.gameObject.SetActive(false);
        foreach(Selectables selectable in _selectedGOList)
        {
            selectable.SetSelectedVisible(false);
        }
        _selectedGOList.Clear();

        _endPos = endPos;
        return CheckSelectables();
    }

    public List<Selectables> CheckSelectables()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPos,_endPos);    

        foreach(Collider2D col in collider2DArray)
        {
            Selectables selectable = col.GetComponent<Selectables>();
            if(selectable != null && selectable.selectableTypes == currentSelection)
            {
                selectable.SetSelectedVisible(true);
                _selectedGOList.Add(selectable);
            }
        }
        return _selectedGOList;
    }
}
