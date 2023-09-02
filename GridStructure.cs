using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
        var cellIndex = GridIndexCalculator(gridPosition);
        if (CheckIndexValidity(cellIndex))
        {
            bool CellIndexBool = true;

            if (grid[cellIndex.y, cellIndex.x].IsTaken == false) //C,C 2x2
            {
                CellIndexBool = false;
                //if (grid[cellIndex.y - 1, cellIndex.x - 1].IsTaken == false) //C-1,//C-1
                //{
                //    if (grid[cellIndex.y - 1, cellIndex.x].IsTaken == false) //C-1,//C
                //    {
                //        if (grid[cellIndex.y, cellIndex.x - 1].IsTaken == false) //C,//C-1
                //        {
                //            CellIndexBool = false;
                //        }
                //    }
                //}
            }

            return CellIndexBool;
        }
        else
        {
            throw new IndexOutOfRangeException("No index" + cellIndex + "in grid");
        }
        //var cellIndex = GridIndexCalculator(gridPosition);
        //if (!CheckIndexValidity(cellIndex))
        //{
        //    throw new IndexOutOfRangeException("No index " + cellIndex + " in grid");
        //}

        //for (int i = -1; i <= 0; i++)
        //{
        //    for (int j = -1; j <= 0; j++)
        //    {
        //        if (grid[cellIndex.y + i, cellIndex.x + j].IsTaken)
        //        {
        //            return true;
        //        }
        //    }
        //}
        //return false;
    }
    public void PlaceStructureOnGrid(GameObject structure, Vector3 gridPosition, StructureBase structureData)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        if (CheckIndexValidity(cellIndex))
        {
            grid[cellIndex.y, cellIndex.x].SetConstruction(structure, structureData); //4,4
            //grid[cellIndex.y - 1, cellIndex.x - 1].SetConstruction(structure); //3,3
            //grid[cellIndex.y - 1, cellIndex.x].SetConstruction(structure); //3,4
            //grid[cellIndex.y, cellIndex.x - 1].SetConstruction(structure); //4,3
            
        }
    }
    public GameObject GetStructureOnGrid(Vector3 gridPosition)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructure();   
    }

    public StructureBase GetStructureDataOnGrid(Vector3 gridPosition)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructureData();
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
        //grid[cellIndex.y - 1, cellIndex.x - 1].RemoveStructure();
        //grid[cellIndex.y - 1, cellIndex.x].RemoveStructure();
        //grid[cellIndex.y, cellIndex.x - 1].RemoveStructure();

        Debug.Log("Grid Position = " + gridPosition);

        Debug.Log("Structure removed at " + cellIndex.y + " , " + cellIndex.x);
        
        Debug.Log("=====================================================================");
    }

    public Vector3Int? GetPositionOfNeighborIfExists(Vector3 gridPosition, Direction direction)
    {
        Vector3Int? NeighborPosition = Vector3Int.FloorToInt(gridPosition);
        switch (direction)
        {
            case Direction.Up:
                NeighborPosition += new Vector3Int(0, 0, cellSize);
                break;
            case Direction.Right:
                NeighborPosition += new Vector3Int(cellSize, 0, 0);
                break;
            case Direction.Down:
                NeighborPosition += new Vector3Int(0, 0, -cellSize);
                break;
            case Direction.Left:
                NeighborPosition += new Vector3Int(-cellSize, 0, 0);
                break;
        }
        var index = GridIndexCalculator(NeighborPosition.Value);
        if(CheckIndexValidity(index) == false)
        {
            return null;
        }
        return NeighborPosition;
    }

    internal HashSet<Vector3Int> GetAllPositionsFromTo(Vector3Int minPoint, Vector3Int maxpoint)
    {
        HashSet<Vector3Int> positionsToReturn = new HashSet<Vector3Int>();
        for(int row = minPoint.z; row <= maxpoint.z; row++)
        {
            for(int column = minPoint.x; column <= maxpoint.x; column++)
            {
                Vector3 gridPosition = GridPosCalculator(new Vector3(column, 0, row));
                positionsToReturn.Add(Vector3Int.FloorToInt(gridPosition));
            }
        }
        return positionsToReturn;
    }

    public IEnumerable<StructureBase> GetAllStructures()
    {
        List<StructureBase> structureDataList = new List<StructureBase>();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                var data = grid[row, col].GetStructureData();
                if(data != null)
                {
                    structureDataList.Add(data);
                }

            }
        }
        return structureDataList;
    }

    public IEnumerable<StructureBase> GetStructureDataInRange(Vector3 gridPosition, int range)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        List<StructureBase> listtoRetrun = new List<StructureBase>();
        if(CheckIndexValidity(cellIndex) == false)
        {
            return listtoRetrun;
        }
        for (int row = cellIndex.y - range; row <= cellIndex.y + range; row++)
        {
            for (int col = cellIndex.x - range; col < cellIndex.x + range; col++)
            {
                var tempPosition = new Vector2Int(col, row);
                if(CheckIndexValidity(tempPosition) && Vector2.Distance(cellIndex, tempPosition) <= range)
                {
                    var data = grid[row, col].GetStructureData();
                    if(data != null)
                    {
                        listtoRetrun.Add(data);
                    }
                }
            }
        }
        return listtoRetrun;
    }

    internal List<Vector3Int> GetStructurePositionInRange(Vector3Int gridPosition, int range)
    {
        var cellIndex = GridIndexCalculator(gridPosition);
        List<Vector3Int> listtoRetrun = new List<Vector3Int>();
        if (CheckIndexValidity(cellIndex) == false)
        {
            return listtoRetrun;
        }
        for (int row = cellIndex.y - range; row <= cellIndex.y + range; row++)
        {
            for (int col = cellIndex.x - range; col <= cellIndex.x + range; col++)
            {
                var tempPosition = new Vector2Int(col, row);
                if (CheckIndexValidity(tempPosition) && Vector2.Distance(cellIndex, tempPosition) <= range)
                {
                    var data = grid[row, col].GetStructureData();
                    if (data != null)
                    {
                        listtoRetrun.Add(GetGridPositionFromIndex(tempPosition));
                    }
                }
            }
        }
        return listtoRetrun;
    }
    public void AddNatureToCell(Vector3 position, GameObject natureElement)
    {
        var gridPosition = GridPosCalculator(position);
        var gridIndex = GridIndexCalculator(gridPosition);
        grid[gridIndex.y, gridIndex.x].AddNatureObject(natureElement);
    }
    public List<GameObject> GetNatureObjectsToRemove(Vector3 position)
    {
        var gridPosition = GridPosCalculator(position);
        var gridIndex = GridIndexCalculator(gridPosition);
        return grid[gridIndex.y, gridIndex.x].GetNatureOnThisCell();
    }

    private Vector3Int GetGridPositionFromIndex(Vector2Int tempPosition)
    {
        return new Vector3Int(tempPosition.x * cellSize, 0, tempPosition.y * cellSize); 
    }

    public bool ArePositionsInRange(Vector3Int gridPosition, Vector3Int structurePositionNearby, int range)
    {
        var distance = Vector2.Distance(GridIndexCalculator(gridPosition), GridIndexCalculator(structurePositionNearby));
        return distance <= range;
    }
}
//Bitwise operation
public enum Direction
{
    Up = 1, //0001
    Right = 2, //0010
    Down = 4, //0100
    Left = 8 //1000
}
