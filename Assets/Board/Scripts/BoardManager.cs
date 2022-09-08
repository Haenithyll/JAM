using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum TilePosition
{
    up,
    center,
    down
}

public class BoardManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Canvas _board;

    [Header("GD")]
    public List<Path> _PathList;

    [Header("Debug")]
    [SerializeField] private Path _currentPath;

    [SerializeField] private int _pathIndex;
    [SerializeField] private int _tileIndex;

    [SerializeField] private Tile _previousTile;
    [SerializeField] private Tile _currentTile;
    [SerializeField] private Tile _nextTile;

    [SerializeField] private GameObject _previousTileGO;
    [SerializeField] private GameObject _currentTileGO;
    [SerializeField] private GameObject _nextTileGO;

    public void InitBoard(int pathIndex)
    {
        _pathIndex = pathIndex;
        _currentPath = _PathList[_pathIndex];
        _currentTile = _currentPath._TileList[0];
        _nextTile = _currentPath._TileList[1];

        int index = 0;
        foreach (Tile tile in _currentPath._TileList)
        {
            float position = _board.GetComponent<RectTransform>().rect.height * (index - (1 + _currentPath._TileList.Count) / 2) / (_currentPath._TileList.Count + 2);
            if (index < _currentPath._TileList.Count - 1)
                SetupNewTile(tile, position);
            else
                SetupBossTile(tile, position);
            index++;
        }
    }

    public void MoveForward()
    {
        _tileIndex++;
        _previousTile = _currentTile;
        _currentTile = _nextTile;
    }

    private void SetupNewTile(Tile tile, float position)
    {
        GameObject tileInstance = Instantiate(tile.gameObject);
        tileInstance.transform.SetParent(_board.gameObject.transform);
        tileInstance.transform.localScale = new Vector3(.75f, .75f, .75f);
        tileInstance.transform.localPosition = new Vector3(0, position, 0);
    }

    private void SetupBossTile(Tile tile, float position)
    {
        GameObject tileInstance = Instantiate(tile.gameObject);
        tileInstance.transform.SetParent(_board.gameObject.transform);
        tileInstance.transform.localScale = new Vector3(1, 1, 1);
        tileInstance.transform.localPosition = new Vector3(0, 1.1f * position, 0);
    }
}
