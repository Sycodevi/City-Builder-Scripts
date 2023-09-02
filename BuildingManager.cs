using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager 
{
    //Cell[,] grid;
    GridStructure grid;
    IPlacementManager placementManager;
    StructureRepo structureRepo;
    StructureModificationScript modification;

    public BuildingManager(GridStructure grid, IPlacementManager placementManager, StructureRepo structureRepo, IResourceManager resourceManager, UIController uiController)
    {
        this.grid = grid;
        this.placementManager = placementManager;
        this.structureRepo = structureRepo;
        StructureModificationFactory.PrepareStructureModificationFactory(structureRepo, grid, placementManager, resourceManager, uiController);
    }
    public void prepareBuildingManager(Type classType)
    {
        modification = StructureModificationFactory.GetScript(classType);
    }

    public void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        modification.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void ConfirmModification()
    {
        modification.ConfirmModifications();
    }

    public void CancelModification()
    {
        modification?.CancelModifications();
    }

    public void PrepareBuildingForRemovalAt(Vector3 inputPosition)
    {
        modification.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }
    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.GridPosCalculator(inputPosition);
        GameObject returnStructure = null;
        returnStructure = modification.AccessStructureInDictionary(gridPosition);
        if(returnStructure != null)
        {
            return returnStructure;
        }
        returnStructure = modification.AccessStructureInDictionary(gridPosition);
        return returnStructure;
    }

    public void StopContinuedPlacement()
    {
        modification.StopContinuedPlacement();
    }

    public IEnumerable<StructureBase> GetAllStructures()
    {
        return grid.GetAllStructures();
    }

    public StructureBase GetStructureDataFromPosition(Vector3 position)
    {
        Vector3 gridPosition = grid.GridPosCalculator(position);
        if(grid.IsCellTaken(gridPosition))
        {
            return grid.GetStructureDataOnGrid(position);
        }
        return null;
    }
}


