using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridStructure
{ 
    private int cellSize;
    Cell[,] grid;
    private int width, lenght;

    public GridStructure(int cellSize, int width, int lenght)
    {
        this.cellSize = cellSize;
        this.width = width;
        this.lenght = lenght;
        grid = new Cell[this.width, this.lenght];

        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for (int column = 0; column < grid.GetLength(1); column++)
            {
                grid[row, column] = new Cell(); 
            }
        }
    }
    public Vector3 GridPosCalculator(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / cellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / cellSize);
        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    public Vector2Int GridIndexCalculator(Vector3 gridPosition)
    {
        return new Vector2Int((int)(gridPosition.x/cellSize),(int)(gridPosition.z/cellSize));
    }
    public bool IsCellTaken(Vector3 gridPosition)
    {
        //var cellIndex = GridIndexCalculator(gridPosition);
        //if (CheckIndexValidity(cellIndex))
        //{
        //    bool CellIndexBool = true;

        //    if (grid[cellIndex.y, cellIndex.x].IsTaken == false) //C,C 2x2
        //    {
        //        if (grid[cellIndex.y - 1, cellIndex.x - 1].IsTaken == false) //C-1,//C-1
        //       {
        //            if (grid[cellIndex.y - 1, cellIndex.x].IsTaken == false) //C-1,//C
        //            {
        //                if (grid[cellIndex.y, cellIndex.x - 1].IsTaken == false) //C,//C-1
        //                {
        //                    CellIndexBool = false;
        //                }
        //            }
        //        }
        //    }

        //    return CellIndexBool;
        //}
        //else
        //{
        //    throw new IndexOutOfRangeException("No index" + cellIndex + "in grid");
        //}
        var cellIndex = GridIndexCalculator(gridPosition);
        if (!CheckIndexValidity(cellIndex))
        {
            throw new IndexOutOfRangeException("No index " + cellIndex + " in grid");
        }

        for (int i = -1; i <= 0; i++)
        {
            for (int j = -1; j <= 0; j++)
            {
                if (grid[cellIndex.y + i, cellIndex.x + j].IsTaken)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void PlaceStructureOnGrid(GameObject structure, Vector3 gridPosition)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        if (CheckIndexValidity(cellIndex))
        {
            grid[cellIndex.y, cellIndex.x].SetConstruction(structure); //4,4
            grid[cellIndex.y - 1, cellIndex.x - 1].SetConstruction(structure); //3,3
            grid[cellIndex.y - 1, cellIndex.x].SetConstruction(structure); //3,4
            grid[cellIndex.y, cellIndex.x - 1].SetConstruction(structure); //4,3


            Debug.Log("Structure Placed at " + cellIndex.y + " , " + cellIndex.x);
            Debug.Log("Structure Placed at " + (cellIndex.y - 1) + " , " + (cellIndex.x - 1));
            Debug.Log("Structure Placed at " + (cellIndex.y - 1) + " , " + (cellIndex.x));
            Debug.Log("Structure Placed at " + cellIndex.y + " , " + (cellIndex.x - 1));
            Debug.Log("=====================================================================");
        }
    }
    public GameObject GetStructureOnGrid(Vector3 gridPosition)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructure();   
    }
    private bool CheckIndexValidity(Vector2 cellIndex)
    {
        if (cellIndex.x >= 0 && cellIndex.x < grid.GetLength(1) && cellIndex.y >= 0 && cellIndex.y < grid.GetLength(0))
            return true;
        else
            return false;
    }

    public void RemoveStructureOnGrid(Vector3 gridPosition)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        grid[cellIndex.y, cellIndex.x].RemoveStructure();
        grid[cellIndex.y - 1, cellIndex.x - 1].RemoveStructure();
        grid[cellIndex.y - 1, cellIndex.x].RemoveStructure();
        grid[cellIndex.y, cellIndex.x - 1].RemoveStructure();

        Debug.Log("Grid Position = " + gridPosition);

        Debug.Log("Structure removed at " + cellIndex.y + " , " + cellIndex.x);
        Debug.Log("Structure removed at " + (cellIndex.y - 1) + " , " + (cellIndex.x - 1));
        Debug.Log("Structure removed at " + (cellIndex.y - 1) + " , " + (cellIndex.x));
        Debug.Log("Structure removed at " + cellIndex.y + " , " + (cellIndex.x - 1));
        Debug.Log("=====================================================================");
    }
}
