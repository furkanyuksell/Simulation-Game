using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputController : MonoBehaviour
{   
    public enum SelectableTypes
    {
        ROCK,GROUND,FOOD,ANIMALS,TREES
    }   
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Camera _camera;
    [SerializeField] private DragSelectionController _dragSelectionController;


    private TileBase _startTile;
    private TileBase _lastTile;
    private Vector3Int _startCellPos;
    private Vector3Int _lastCellPos;
    private SelectableTypes currentSelection;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startCellPos = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
            _dragSelectionController.SelectionStarted(_tileMap.GetCellCenterWorld(_startCellPos),SelectableTypes.TREES);
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
                _dragSelectionController.SelectionEnded(_tileMap.GetCellCenterWorld(_lastCellPos));

                _startTile = null;
                _lastTile = null;
            }
        }
    }
}
