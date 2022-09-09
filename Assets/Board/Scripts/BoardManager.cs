using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private List<GameObject> _InstantiatedTiles;

    public void InitBoard(int pathIndex)
    {
        _tileIndex = 0;
        _pathIndex = pathIndex;
        _currentPath = _PathList[_pathIndex];

        int index = 0;
        foreach (Tile tile in _currentPath._TileList)
        {
            float position = _board.GetComponent<RectTransform>().rect.height * (index - (1 + _currentPath._TileList.Count) / 2) / (_currentPath._TileList.Count + 2);
            if (index < _currentPath._TileList.Count - 1)
                _InstantiatedTiles.Add(SetupNewTile(tile, position));
            else
                _InstantiatedTiles.Add(SetupBossTile(tile, position));
            index++;
        }
    }

    private GameObject SetupNewTile(Tile tile, float position)
    {
        GameObject tileInstance = Instantiate(tile.gameObject);

        tileInstance.transform.SetParent(_board.gameObject.transform);
        tileInstance.transform.localScale = new Vector3(.75f, .75f, .75f);
        tileInstance.transform.localPosition = new Vector3(0, position, 0);

        return tileInstance;
    }

    private GameObject SetupBossTile(Tile tile, float position)
    {
        GameObject tileInstance = Instantiate(tile.gameObject);

        tileInstance.transform.SetParent(_board.gameObject.transform);
        tileInstance.transform.localScale = new Vector3(1, 1, 1);
        tileInstance.transform.localPosition = new Vector3(0, 1.1f * position, 0);

        return tileInstance;
    }

    public Vector3 GetDestinationTilePosition()
    {
        Vector3 positionToReturn = _InstantiatedTiles[_tileIndex].transform.position;
        //positionToReturn += new Vector3(0, _tileIndex*11, 0);
        _tileIndex++;
        if (_tileIndex == _currentPath._TileList.Count) 
            GameManager.instance.QueueBoss();
        return positionToReturn;
    }

    public void DisplayText(TextMeshProUGUI textHolder)
    {
        _currentPath._TileList[_tileIndex-1].DisplayText(textHolder);
    }
}
