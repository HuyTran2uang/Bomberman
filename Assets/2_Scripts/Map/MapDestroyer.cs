using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class MapDestroyer : MonoBehaviourSingleton<MapDestroyer>
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Tile[] _walls;
    [SerializeField] private Tile[] _bushes;
    [SerializeField] private Tile[] _chests;
    [SerializeField] private GameObject _explodePrefabs;
    [SerializeField] private GameObject[] _itemPrefabs;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private GameObject GetRandomItem()
    {
        int index = Random.Range(0, _itemPrefabs.Length);
        return _itemPrefabs[index];
    }

    [PunRPC]
    public void ExplodeBomb(Vector3 worldPos, int forceExplode)
    {
        Vector3Int originCell = _tileMap.WorldToCell(worldPos);
        ExplodeCell(originCell, false, Vector2.zero, Vector2.zero);
        int n = forceExplode;
        for (int i = 1; i <= n; i++)
        {
            if (i == n)
            {
                if (!ExplodeCell(originCell + Vector3Int.right * i, true, Vector2.right, Vector2.right)) break;
            }
            else
            {
                if (!ExplodeCell(originCell + Vector3Int.right * i, false, Vector2.right, Vector2.right)) break;
            }
        }
        for (int i = 1; i <= n; i++)
        {
            if (i == n)
            {
                if (!ExplodeCell(originCell + Vector3Int.left * i, true, Vector2.left, Vector2.left)) break;
            }
            else
            {
                if (!ExplodeCell(originCell + Vector3Int.left * i, false, Vector2.left, Vector2.left)) break;
            }
        }
        for (int i = 1; i <= n; i++)
        {
            if (i == n)
            {
                if (!ExplodeCell(originCell + Vector3Int.up * i, true, Vector2.up, Vector2.up)) break;
            }
            else
            {
                if (!ExplodeCell(originCell + Vector3Int.up * i, false, Vector2.up, Vector2.up)) break;
            }
        }
        for (int i = 1; i <= n; i++)
        {
            if (i == n)
            {
                if (!ExplodeCell(originCell + Vector3Int.down * i, true, Vector2.down, Vector2.down)) break;
            }
            else
            {
                if (!ExplodeCell(originCell + Vector3Int.down * i, false, Vector2.down, Vector2.down)) break;
            }
        }
    }

    private bool ExplodeCell(Vector3Int cell, bool checkLast, Vector2 move, Vector2 lastMove)
    {
        Tile tile = _tileMap.GetTile<Tile>(cell);
        bool canAppearItems = false;

        foreach (Tile wall in _walls)
        {
            if (tile == wall) return false;
        }

        foreach (Tile bush in _bushes)
        {
            if (tile == bush)
            {
                _tileMap.SetTile(cell, null);
                canAppearItems = true;
            }
        }

        foreach (Tile chest in _chests)
        {
            if (tile == chest)
            {
                _tileMap.SetTile(cell, null);
                canAppearItems = true;
                StartCoroutine(InitExplode(cell, checkLast, move, lastMove, canAppearItems));
                return false;
            }
        }

        StartCoroutine(InitExplode(cell, checkLast, move, lastMove, canAppearItems));
        return true;
    }

    private IEnumerator InitExplode(Vector3Int cell, bool checkLast, Vector2 move, Vector2 lastMove, bool canAppearItems)
    {
        Vector3 pos = _tileMap.GetCellCenterWorld(cell);
        GameObject explodeClone = Instantiate(_explodePrefabs, pos, Quaternion.identity);
        explodeClone.GetComponent<Animator>().SetBool("isLast", checkLast);
        explodeClone.GetComponent<Animator>().SetFloat("moveX", move.x);
        explodeClone.GetComponent<Animator>().SetFloat("moveY", move.y);
        explodeClone.GetComponent<Animator>().SetFloat("lastMoveX", lastMove.x);
        explodeClone.GetComponent<Animator>().SetFloat("lastMoveY", lastMove.y);
        yield return new WaitForSeconds(0.5f);
        Destroy(explodeClone);

        if (canAppearItems)
        {
            int index = MapManager.Instance.GetSumIJ(pos) % _itemPrefabs.Length;
            GameObject item = _itemPrefabs[index];
            if (item != null) Instantiate(item, pos, Quaternion.identity);
        }
    }
}
