using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public enum SelectableTypes
    {
        MINER, GROUND, GATHERER, HUNTER, WOODSMAN
    }
    
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Camera _camera;
    [SerializeField] private DragSelectionController _dragSelectionController;
    [SerializeField] private TopPanelController _topPanelController;


    private TileBase _startTile;
    private TileBase _lastTile;
    private Vector3Int _startCellPos;
    private Vector3Int _lastCellPos;
    public SelectableTypes currentSelection;
    public bool _inputEnabled = false;
    public bool _selectionEnabled = false;
    private List<Selectables> _selectedGOList;

    void Update()
    {
        if (_selectionEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startCellPos = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
                _dragSelectionController.SelectionStarted(_tileMap.GetCellCenterWorld(_startCellPos), currentSelection);
                if (_tileMap.HasTile(_startCellPos))
                {
                    _startTile = _tileMap.GetTile(_startCellPos);
                }
                else
                {
                    _startTile = null;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                _lastCellPos = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
                if (_tileMap.HasTile(_lastCellPos))
                {
                    _lastTile = _tileMap.GetTile(_startCellPos);
                }
                else
                {
                    _lastTile = null;
                }
                _dragSelectionController.SetSelectionPosition(_tileMap.GetCellCenterWorld(_lastCellPos));
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (_startTile != null && _lastTile != null)
                {
                    _selectedGOList = _dragSelectionController.SelectionEnded(_tileMap.GetCellCenterWorld(_lastCellPos));
                    
                    // Game Objects are here. We should work on them.
                    
                    _startTile = null;
                    _lastTile = null;
                    if(_selectedGOList.Count > 0)
                    {
                        _selectionEnabled = false;
                        Invoke("InputEnabled",0.1f);
                    }
                }
            }
        }
        if(Input.GetMouseButtonDown(0) && _inputEnabled)
        {
            foreach(Selectables selectable in _selectedGOList)
            {
                selectable.SetSelectedVisible(false);
            }
            _selectedGOList.Clear();
            _inputEnabled = false;
        }
    }

    public void SelectionEnable()
    {
        Invoke("SelectionEnabled",0.1f);
    }
    
    public void InputEnabled()
    {
        _inputEnabled = true;
    }

    public void SelectionEnabled()
    {
        _selectionEnabled = true;
    }
}
