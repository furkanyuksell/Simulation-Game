using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputController : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _tileSelectedGO;

    private SpriteRenderer _tileSelectedSR;

    private TileBase _startTile;
    private TileBase _lastTile;
    private Vector3 _worldStartPos;
    private Vector3 _worldLastPos;
    private Vector3Int _startPos;
    private Vector3Int _lastPos;

    private int _size = 0;


    private void Awake()
    {
        _tileSelectedSR = _tileSelectedGO.GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
            _tileSelectedGO.transform.position = _tileMap.GetCellCenterWorld(_startPos);
            _tileSelectedSR.size = new Vector2(1,1);
            _tileSelectedGO.SetActive(true);
            if (_tileMap.HasTile(_startPos))
            {
                _startTile = _tileMap.GetTile(_startPos);
            }
            else
            {
                _startTile = null;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_startTile != null && _lastTile != null)
            {
                Vector3Int dist = _startPos - _lastPos;
                int matStartPosX;
                int matStartPosY;
                int matLastPosX;
                int matLastPosY;

                int selectedXSize;
                int selectedYSize;


                if (_startPos.x < _lastPos.x)
                {
                    matStartPosX = _startPos.x;
                    matLastPosX = _lastPos.x;
                    selectedXSize = -1;
                }
                else
                {
                    matStartPosX = _lastPos.x;
                    matLastPosX = _startPos.x;
                    selectedXSize = +1;
                }
                if (_startPos.y < _lastPos.y)
                {
                    matStartPosY = _startPos.y;
                    matLastPosY = _lastPos.y;
                    selectedYSize = -1;
                }
                else
                {
                    matStartPosY = _lastPos.y;
                    matLastPosY = _startPos.y;
                    selectedYSize = +1;
                }
                if(_tileSelectedSR.size != new Vector2(selectedXSize + dist.x, selectedYSize + dist.y))
                {
                    _tileSelectedSR.size = new Vector2(selectedXSize + dist.x, selectedYSize + dist.y);
                    _tileSelectedGO.transform.position =  (_tileMap.GetCellCenterWorld(_startPos) + _tileMap.GetCellCenterWorld(_lastPos))/2;
                }
            }
            _lastPos = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
            if (_tileMap.HasTile(_lastPos))
            {
                _lastTile = _tileMap.GetTile(_startPos);
            }
            else
            {
                _lastTile = null;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_startTile != null && _lastTile != null)
            {
                Vector3Int dist = _startPos - _lastPos;
                int matStartPosX = (_startPos.x < _lastPos.x) ? _startPos.x : _lastPos.x;
                int matStartPosY = (_startPos.y < _lastPos.y) ? _startPos.y : _lastPos.y;
                for (int x = matStartPosX; x <= matStartPosX + Mathf.Abs(dist.x); x++)
                {
                    for (int y = matStartPosY; y <= matStartPosY + Mathf.Abs(dist.y); y++)
                    {
                        Debug.Log("Grid position: " + x + "," + y);
                    }
                }
                _startTile = null;
                _lastTile = null;
                _tileSelectedGO.SetActive(false);
            }
        }
    }
}
