using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottomPanelController : MonoBehaviour
{
    private bool isInputWaiting;
    private UnityAction activityControl;
    
    private void DigControl()
    {

    }
    private void ChopControl()
    {

    }
    private void GatherControl()
    {

    }
    private void HoeControl()
    {

    }
    private void HuntControl()
    {

    }

    private void OnClick()
    {
        isInputWaiting = true;
        Debug.Log(activityControl);
    }
    public void Dig()
    {
        activityControl = Dig;
        OnClick();
    }
    public void Chop()
    {
        activityControl = ChopControl;
        OnClick();
    }
    public void Gather()
    {
        activityControl = GatherControl;
        OnClick();
    }
    public void Hoe()
    {
        activityControl = HoeControl;
        OnClick();
    }
    public void Hunt()
    {
        activityControl = HuntControl;
        OnClick();
    }
}
