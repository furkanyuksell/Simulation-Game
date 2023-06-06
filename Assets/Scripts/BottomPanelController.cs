using UnityEngine;

public class BottomPanelController : MonoBehaviour
{

    [SerializeField] private InputController _inputController;
    
    private void OnClick()
    {
        _inputController.SelectionEnable();
    }

    public void Dig()
    {
        _inputController.currentSelection = InputController.SelectableTypes.MINER;
        OnClick();
    }
    public void Chop()
    {
        _inputController.currentSelection = InputController.SelectableTypes.WOODSMAN;
        OnClick();
    }
    public void Gather()
    {
        _inputController.currentSelection = InputController.SelectableTypes.GATHERER;
        OnClick();
    }
    public void Hoe()
    {
        _inputController.currentSelection = InputController.SelectableTypes.GROUND;
        OnClick();
    }
    public void Hunt()
    {
        _inputController.currentSelection = InputController.SelectableTypes.HUNTER;
        OnClick();
    }
}
