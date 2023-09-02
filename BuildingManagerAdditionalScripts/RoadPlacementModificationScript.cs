
using System.Collections.Generic;
using UnityEngine;
using static GridStructure;
public class RoadPlacementModificationScript : StructureModificationScript
{
    Dictionary<Vector3Int, GameObject> ExistingRoadStructuresToBeChanged = new Dictionary<Vector3Int, GameObject>();
    public RoadPlacementModificationScript(StructureRepo structureRepo, GridStructure grid, IPlacementManager placementManager, IResourceManager resourceManager, UIController uiController) : base(structureRepo, grid, placementManager, resourceManager, uiController)
    {
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPosition = grid.GridPosCalculator(inputPosition);
        if(grid.IsCellTaken(gridPosition) == false)
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var roadStructure = GetCorrectRoadPrefab(gridPosition, structureData);
            if (structureToBeModified.ContainsKey(gridPositionInt))
            {
                RevokePlacementAt(gridPositionInt);
                resourceManager.IncreaseMoney(structureData.placementCost);
            }
            else if(resourceManager.Purchasable(structureData.placementCost))
            {
                PlaceNewRoadAt(roadStructure, gridPosition, gridPositionInt);
                resourceManager.SpendMoney(structureData.placementCost);
            }
            AdjustNeighborsIfAreRoadStructure(gridPosition);
        }
    }

    private void AdjustNeighborsIfAreRoadStructure(Vector3 gridPosition)
    {
        AdjustNeighborsIfRoad(gridPosition, Direction.Up);
        AdjustNeighborsIfRoad(gridPosition, Direction.Down);
        AdjustNeighborsIfRoad(gridPosition, Direction.Right);
        AdjustNeighborsIfRoad(gridPosition, Direction.Left);
    }

    private void AdjustNeighborsIfRoad(Vector3 gridPosition, Direction direction)
    {
        var neighborGridPosition = grid.GetPositionOfNeighborIfExists(gridPosition, direction);
        if(neighborGridPosition.HasValue)
        {
            var neighborPositionInt = neighborGridPosition.Value;
            AdjustStructureIfInDictionary(neighborGridPosition, neighborPositionInt);
            AdjustStructureIfOnGrid(neighborGridPosition, neighborPositionInt);
        }
    }

    private void AdjustStructureIfOnGrid(Vector3Int? neighborGridPosition, Vector3Int neighborPositionInt)
    {
        if (RoadManager.CheckIfNeighborIsRoadOnTheGrid(grid, neighborPositionInt))
        {
            var neighborStructureData = grid.GetStructureDataOnGrid(neighborGridPosition.Value);
            if (neighborStructureData != null && neighborStructureData.GetType() == typeof(RoadStructureObj) && ExistingRoadStructuresToBeChanged.ContainsKey(neighborPositionInt) == false)
            {
                ExistingRoadStructuresToBeChanged.Add(neighborPositionInt, grid.GetStructureOnGrid(neighborGridPosition.Value));
            }
        }
    }

    private void AdjustStructureIfInDictionary(Vector3Int? neighborGridPosition, Vector3Int neighborPositionInt)
    {
        if (RoadManager.CheckIfNeighborIsRoadInDictionary(neighborPositionInt, structureToBeModified))
        {
            RevokePlacementAt(neighborPositionInt);
            var neighborStructure = GetCorrectRoadPrefab(neighborGridPosition.Value, structureData);
            PlaceNewRoadAt(neighborStructure, neighborGridPosition.Value, neighborPositionInt);
        }
    }

    private void PlaceNewRoadAt(RoadStructureScript roadStructure, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        Quaternion newRotation = Quaternion.Euler(0,0,0);
        switch (roadStructure.RoadPrefabRotation)
        {
            case RotationValue.Rotate0:
                newRotation = Quaternion.Euler(0, 0, 0);
                break;
            case RotationValue.Rotate90:
                newRotation = Quaternion.Euler(0, 90, 0);
                break;
            case RotationValue.Rotate180:
                newRotation = Quaternion.Euler(0, 180, 0);
                break;
            case RotationValue.Rotate270:
                newRotation = Quaternion.Euler(0, 270, 0);
                break;
            default:
                break;
        }
        
        structureToBeModified.Add(gridPositionInt, placementManager.CreateGhostStructure(gridPosition, roadStructure.RoadPrefab, newRotation/*roadStructure.RoadPrefabRotation*/));
    }

    private void RevokePlacementAt(Vector3Int gridPositionInt)
    {
        var structure = structureToBeModified[gridPositionInt];
        placementManager.RemoveSingleStructure(structure);
        structureToBeModified.Remove(gridPositionInt);
    }

    private RoadStructureScript GetCorrectRoadPrefab(Vector3 gridPosition, StructureBase structureData)
    {
        var neighborStatus = RoadManager.GetRoadNeighborStatus(gridPosition, grid, structureToBeModified);
        RoadStructureScript roadToReturn = null;
        roadToReturn = RoadManager.CheckIfStraightRoadFits(neighborStatus, roadToReturn, structureData);
        if(roadToReturn != null)
            return roadToReturn;
        roadToReturn = RoadManager.CheckIfCornerFits(neighborStatus, roadToReturn, structureData);
        if (roadToReturn != null)
            return roadToReturn;
        roadToReturn = RoadManager.CheckIfThreewayFits(neighborStatus, roadToReturn, structureData);
        if (roadToReturn != null)
            return roadToReturn;
        roadToReturn = RoadManager.CheckIfFourwayFits(neighborStatus, roadToReturn, structureData);
        return roadToReturn;
    }
    public override void CancelModifications()
    {
        resourceManager.IncreaseMoney(structureToBeModified.Count * structureData.placementCost);
        base.CancelModifications();
        ExistingRoadStructuresToBeChanged.Clear();
    }
    public override void ConfirmModifications()
    {
        ModifyRoadCellsOnGrid(ExistingRoadStructuresToBeChanged, structureData);
        base.ConfirmModifications();
    }

    public void ModifyRoadCellsOnGrid(Dictionary<Vector3Int, GameObject> neighborDictionary, StructureBase structureData)
    {

        foreach (var keyValuePair in neighborDictionary)
        {
            grid.RemoveStructureOnGrid(keyValuePair.Key);
            placementManager.RemoveSingleStructure(keyValuePair.Value);
            var roadStructure = GetCorrectRoadPrefab(keyValuePair.Key, structureData);
            Quaternion newRotation = Quaternion.Euler(0, 0, 0);
            switch (roadStructure.RoadPrefabRotation)
            {
                case RotationValue.Rotate0:
                    newRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case RotationValue.Rotate90:
                    newRotation = Quaternion.Euler(0, 90, 0);
                    break;
                case RotationValue.Rotate180:
                    newRotation = Quaternion.Euler(0, 180, 0);
                    break;
                case RotationValue.Rotate270:
                    newRotation = Quaternion.Euler(0, 270, 0);
                    break;
                default:
                    break;
            }
            var structure = placementManager.PlaceStructureOnMap(keyValuePair.Key, roadStructure.RoadPrefab, newRotation/*roadStructure.RoadPrefabRotation*/);
            grid.PlaceStructureOnGrid(structure, keyValuePair.Key, GameObject.Instantiate(structureData));
        }
        neighborDictionary.Clear();
    }
}
