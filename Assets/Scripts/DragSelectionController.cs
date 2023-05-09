using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragSelectionController : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;
    private UnityAction<Vector3> currentAction; 
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
        switch(type)
        {
            case InputController.SelectableTypes.ROCK:
                currentAction = CheckRocks;
                break;
            case InputController.SelectableTypes.GROUND:
                currentAction = CheckGround;
                break;
            case InputController.SelectableTypes.FOOD:
                currentAction = CheckFoods;
                break;
            case InputController.SelectableTypes.ANIMALS:
                currentAction = CheckAnimals;
                break;
            case InputController.SelectableTypes.TREES:
                currentAction = CheckTrees;
                break;                
        }
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

    public void SelectionEnded(Vector3 endPos)
    {
        _selectionAreaTransform.gameObject.SetActive(false);
        foreach(Selectables selectable in _selectedGOList)
        {
            selectable.SetSelectedVisible(false);
        }
        _selectedGOList.Clear();

        _endPos = endPos;
        currentAction?.Invoke(endPos);
    }

    public void CheckAnimals(Vector3 endPos)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPos,_endPos);    

        foreach(Collider2D col in collider2DArray)
        {
            Selectables selectable = col.GetComponent<SelectableAnimals>();
            if(selectable != null)
            {
                selectable.SetSelectedVisible(true);
                _selectedGOList.Add(selectable);
                Debug.Log(gameObject);
            }
        }
    }
    public void CheckTrees(Vector3 endPos)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPos,_endPos);    

        int i= 0;
        foreach(Collider2D col in collider2DArray)
        {
            Selectables selectable = col.GetComponent<SelectableTrees>();
            if(selectable != null)
            {
                selectable.SetSelectedVisible(true);
                _selectedGOList.Add(selectable);
                Debug.Log(gameObject  + " " + i);
            }
        }
    }
    public void CheckFoods(Vector3 endPos)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPos,_endPos);    

        foreach(Collider2D col in collider2DArray)
        {
            Selectables selectable = col.GetComponent<SelectableFood>();
            if(selectable != null)
            {
                selectable.SetSelectedVisible(true);
                _selectedGOList.Add(selectable);
                Debug.Log(gameObject);
            }
        }
    }

    public void CheckRocks(Vector3 endPos)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPos,_endPos);    

        foreach(Collider2D col in collider2DArray)
        {
            Selectables selectable = col.GetComponent<SelectableRock>();
            if(selectable != null)
            {
                selectable.SetSelectedVisible(true);
                _selectedGOList.Add(selectable);
                Debug.Log(gameObject);
            }
        }
    }
    public void CheckGround(Vector3 endPos)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPos,_endPos);    

        foreach(Collider2D col in collider2DArray)
        {
            Selectables selectable = col.GetComponent<SelectableGround>();
            if(selectable != null)
            {
                selectable.SetSelectedVisible(true);
                _selectedGOList.Add(selectable);
                Debug.Log(gameObject);
            }
        }
    }

}
