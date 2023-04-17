using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragSelectionController : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;
    private UnityAction<Vector3> currentAction; 

    public void SelectionStarted(Vector3 startPos,Selectables.SelectableTypes type)
    {
        _startPos = startPos;
        switch(type)
        {
            case Selectables.SelectableTypes.ROCK:
                currentAction = CheckRocks;
                break;
            case Selectables.SelectableTypes.GROUND:
                currentAction = CheckGround;
                break;
            case Selectables.SelectableTypes.FOOD:
                currentAction = CheckFoods;
                break;
            case Selectables.SelectableTypes.ANIMALS:
                currentAction = CheckAnimals;
                break;
            case Selectables.SelectableTypes.TREES:
                currentAction = CheckTrees;
                break;                
        }
    }

    public void SetSelectionPosition()
    {

    }

    public void SelectionEnded(Vector3 endPos)
    {
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
                Debug.Log(gameObject);
            }
        }
    }

}
