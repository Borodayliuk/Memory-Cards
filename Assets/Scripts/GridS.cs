using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridS : MonoBehaviour
{
    private Vector3 _gridSize;
    private Vector3Int _cellCount;
    private GameObject _parent;
    private Bounds _bounds;
    
    public void GenerationGrid(GameObject[,] cards, Vector3Int cellCount)
    {
        _bounds = new Bounds();
        _parent = new GameObject();
        _parent.transform.position = Vector3.zero;
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;

        _gridSize = new Vector3(width - (width / 3), height, 1);
        _cellCount = cellCount;
        _cellCount.x = cards.GetLength(0);
        _cellCount.y = cards.GetLength(1);

        for (int x = 0; x < _cellCount.x; x++)
        {
            for (int y = 0; y < _cellCount.y; y++)
            {

                cards[x, y].transform.position = GridToWorld(new Vector3Int(x, y, 1));
                cards[x, y].transform.SetParent(_parent.transform);
                _bounds.Encapsulate(cards[x, y].transform.position);
            }
        }
        _parent.transform.position = -_bounds.size / 2 - new Vector3(_bounds.size.x / 2.6f, 0, 0);
        
    } 
    public Vector3 GridToWorld(Vector3Int gridPos)
    {
        var result = Vector3.zero;
        result = Vector3.Scale(gridPos, GetCellSizeFromRect(_gridSize, _cellCount));
        return result;
    }
    public Vector3 GetCellSizeFromRect(Vector3 gridSize, Vector3Int size)
    {
        var result = Vector3.zero;
        result = new Vector3(gridSize.x / (size.x  + 1), gridSize.y / (size.x ), gridSize.z / size.z);
        return result;
    }
   
}
