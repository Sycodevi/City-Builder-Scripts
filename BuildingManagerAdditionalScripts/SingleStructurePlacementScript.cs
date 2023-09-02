using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SingleStructurePlacementScript : StructureModificationScript
{
    public SingleStructurePlacementScript(StructureRepo structureRepo, GridStructure grid, IPlacementManager placementManager, IResourceManager resourceManager, UIController uiController) : base(structureRepo, grid, placementManager, resourceManager, uiController)
    {
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Quaternion newRotation = Quaternion.Euler(0,0,0);
        //GameObject buildingPrefab = this.structureRepo.GetBuildingPrefab(structureName, structureType);
        if (uiController.rotateVal == 0)
        {
            newRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (uiController.rotateVal == 1)
        {
            newRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (uiController.rotateVal == 2)
        {
            newRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (uiController.rotateVal == 3)
        {
            newRotation = Quaternion.Euler(0, 270, 0);
        }
        GameObject buildingPrefab = structureData.prefab;
        Vector3 gridPosition = grid.GridPosCalculator(inputPosition);
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            if (structureToBeModified.ContainsKey(gridPositionInt))
            {
                resourceManager.IncreaseMoney(structureData.placementCost);
                RevokeStructurePlacement(gridPositionInt);
            }
            else if(resourceManager.Purchasable(structureData.placementCost))
            {
                PlaceStructureAt(buildingPrefab, gridPosition, gridPositionInt, newRotation);
                Debug.Log("gridPostiion: " + gridPosition + "\ngridPostionInt: " + grid);
                resourceManager.SpendMoney(structureData.placementCost);
            }
            //placementManager.CreateBuilding(gridPosition, grid, buildingPrefab);

        }
    }

    private void PlaceStructureAt(GameObject buildingPrefab, Vector3 gridPosition, Vector3Int gridPositionInt, Quaternion quaternion)
    {
        structureToBeModified.Add(gridPositionInt, placementManager.CreateGhostStructure(gridPosition, buildingPrefab, quaternion));
    }

    private void RevokeStructurePlacement(Vector3Int gridPositionInt)
    {
        var structure = structureToBeModified[gridPositionInt];
        placementManager.RemoveSingleStructure(structure);
        structureToBeModified.Remove(gridPositionInt);
    }

    public override void CancelModifications()
    {
        resourceManager.IncreaseMoney(structureToBeModified.Count * structureData.placementCost);

        base.CancelModifications();
    }
}
