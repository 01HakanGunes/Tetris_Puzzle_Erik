using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public int rows = 4;
    public int cols = 4;
    public int cellSize = 1;
    public List<Transform> gridCells = new List<Transform>();
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2 position = new Vector2(x * cellSize, -y * cellSize);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                gridCells.Add(cell.transform);

            }
        }
    }

}
