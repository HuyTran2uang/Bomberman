using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviourSingleton<MapManager>
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Transform _firstPosition;
    private Vector3Int[,] _positionsCell;
    private Vector3[,] _positionsCenter;
    private bool[,] _canPutBomb;
    private int _column;
    private int _row;
    private float _scaleCell;

    private void Awake()
    {
        _tilemap = transform.Find("GamePlay").GetComponent<Tilemap>();
    }

    private void Start()
    {
        _column = 22;
        _row = 12;
        _scaleCell = 0.16f;
        SetupMap();
    }

    public Tilemap Tilemap
    {
        get { return _tilemap; }
    }

    private void SetupMap()
    {
        _positionsCell = new Vector3Int[_row, _column];
        _positionsCenter = new Vector3[_row, _column];
        _canPutBomb = new bool[_row, _column];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                Vector3 curPos = _firstPosition.position + Vector3.right * j * _scaleCell + Vector3.down * i * _scaleCell;
                _positionsCell[i, j] = _tilemap.WorldToCell(curPos);
                _positionsCenter[i, j] = _tilemap.GetCellCenterWorld(_positionsCell[i, j]);
                _canPutBomb[i, j] = false;
            }
        }
    }

    public bool CheckBombPlaced(Vector3 pos)
    {
        Vector3Int cell = _tilemap.WorldToCell(pos);
        Vector3 center = _tilemap.GetCellCenterWorld(cell);
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (center == _positionsCenter[i, j])
                {
                    return _canPutBomb[i, j];
                }
            }
        }
        return true;
    }

    public bool CheckBombPlacedCell(Vector3Int cell)
    {
        Vector3 center = _tilemap.GetCellCenterWorld(cell);
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (center == _positionsCenter[i, j])
                {
                    return _canPutBomb[i, j];
                }
            }
        }
        return true;
    }

    public int GetSumIJ(Vector3 pos)
    {
        Vector3Int cell = _tilemap.WorldToCell(pos);
        Vector3 center = _tilemap.GetCellCenterWorld(cell);
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (center == _positionsCenter[i, j])
                {
                    return i + j;
                }
            }
        }
        return 0;
    }

    public void SetBombPlaced(Vector3 pos, bool value)
    {
        Vector3Int cell = _tilemap.WorldToCell(pos);
        Vector3 center = _tilemap.GetCellCenterWorld(cell);
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (center == _positionsCenter[i, j])
                {
                    _canPutBomb[i, j] = value;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (!_canPutBomb[i, j])
                {
                    Gizmos.DrawWireCube(_positionsCenter[i, j], Vector2.one * 0.12f);
                }
            }
        }
    }
}
