using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoadManager
{
    public static int GetRoadNeighborStatus(Vector3 gridPosition, GridStructure grid, Dictionary<Vector3Int, GameObject> structureToBeModified)
    {
        int roadNeighborStatus = 0;
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var neighborPosition = grid.GetPositionOfNeighborIfExists(gridPosition, direction);
            //if (neighborPosition.HasValue && grid.IsCellTaken(neighborPosition.Value))
            //{
            //    var neighborStructureData = grid.GetStructureDataOnGrid(neighborPosition.Value);
            //    if (neighborStructureData != null || CheckDictionaryForRoadAtNeighbor(neighborPosition.Value, structureToBeModified))
            //    {
            //        roadNeighborStatus += (int)direction;
            //    }
            //}
            if(neighborPosition.HasValue)
            {
                if(CheckIfNeighborIsRoadOnTheGrid( grid, neighborPosition) || CheckIfNeighborIsRoadInDictionary(neighborPosition, structureToBeModified))
                {
                    roadNeighborStatus += (int)direction;
                }
                
            }
        }
        return roadNeighborStatus;
    }
    public static bool CheckIfNeighborIsRoadOnTheGrid(GridStructure grid, Vector3Int? neighborPosition)
    {
        if (grid.IsCellTaken(neighborPosition.Value))
        {
            var neighborStructureData = grid.GetStructureDataOnGrid(neighborPosition.Value);
            if (neighborStructureData != null && neighborStructureData.GetType() == typeof(RoadStructureObj))
            {
                return true;
            }
        }
        return false;
    }
    public static bool CheckIfNeighborIsRoadInDictionary(Vector3Int? neighborPosition, Dictionary<Vector3Int, GameObject> structureToBeModified)
    {
        return structureToBeModified.ContainsKey(neighborPosition.Value);

    }

    internal static RoadStructureScript CheckIfStraightRoadFits(int neighborStatus, RoadStructureScript roadToReturn, StructureBase structureData)
    {
        if(neighborStatus ==((int)Direction.Up | (int)Direction.Down) || neighborStatus == (int)Direction.Up || neighborStatus == (int)Direction.Down)
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).prefab, RotationValue.Rotate90);
        }
        else if(neighborStatus == ((int)Direction.Right | (int)Direction.Left) || neighborStatus == (int)Direction.Right || neighborStatus == (int)Direction.Left || neighborStatus == 0)
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).prefab, RotationValue.Rotate0);
        }
        return roadToReturn;
    }

    internal static RoadStructureScript CheckIfCornerFits(int neighborStatus, RoadStructureScript roadToReturn, StructureBase structureData)
    {
        if(neighborStatus == ((int)Direction.Up | (int)Direction.Right)){
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).cornerPrefab, RotationValue.Rotate0);
        }
        else if(neighborStatus == ((int)Direction.Down | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).cornerPrefab, RotationValue.Rotate90);
        }
        else if (neighborStatus == ((int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).cornerPrefab, RotationValue.Rotate180);
        }
        else if (neighborStatus == ((int)Direction.Up | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).cornerPrefab, RotationValue.Rotate270);
        }
        return roadToReturn;
    }

    internal static RoadStructureScript CheckIfThreewayFits(int neighborStatus, RoadStructureScript roadToReturn, StructureBase structureData)
    {
        if (neighborStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).threewayPrefab, RotationValue.Rotate90);
        }
        else if (neighborStatus == ((int)Direction.Left | (int)Direction.Up | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).threewayPrefab, RotationValue.Rotate0);
        }
        else if (neighborStatus == ((int)Direction.Down | (int)Direction.Left | (int)Direction.Up))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).threewayPrefab, RotationValue.Rotate270);
        }
        else if (neighborStatus == ((int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).threewayPrefab, RotationValue.Rotate180);
        }
        return roadToReturn;
    }

    internal static RoadStructureScript CheckIfFourwayFits(int neighborStatus, RoadStructureScript roadToReturn, StructureBase structureData)
    {
        if (neighborStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureScript(((RoadStructureObj)structureData).fourwayPrefab, RotationValue.Rotate0);
        }
        return roadToReturn;
    }

    public static Dictionary<Vector3Int, GameObject> GetRoadNeighborForPosition(GridStructure grid, Vector3Int position)
    {
        Dictionary<Vector3Int, GameObject> dictionaryToReturn = new Dictionary<Vector3Int, GameObject>();
        List<Vector3Int?> neighborPossibleLocations = new List<Vector3Int?>();
        neighborPossibleLocations.Add(grid.GetPositionOfNeighborIfExists(position, Direction.Up));
        neighborPossibleLocations.Add(grid.GetPositionOfNeighborIfExists(position, Direction.Down));
        neighborPossibleLocations.Add(grid.GetPositionOfNeighborIfExists(position, Direction.Left));
        neighborPossibleLocations.Add(grid.GetPositionOfNeighborIfExists(position, Direction.Right));
        foreach(var possibleLocation in neighborPossibleLocations)
        {
            if (possibleLocation.HasValue) 
            {
                if(CheckIfNeighborIsRoadOnTheGrid(grid, possibleLocation.Value) && dictionaryToReturn.ContainsKey(possibleLocation.Value) == false)
                {
                    dictionaryToReturn.Add(possibleLocation.Value, grid.GetStructureOnGrid(possibleLocation.Value));
                }
            }
        } return dictionaryToReturn;
    }
}
