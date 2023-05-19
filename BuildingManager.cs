using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager 
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepo structureRepo;

    public BuildingManager(int cellsize, int width, int lenght, PlacementManager placementManager, StructureRepo structureRepo)
    {
        this.grid = new GridStructure(cellsize, width, lenght);
        this.placementManager = placementManager;
        this.structureRepo = structureRepo;
    }

    public void PlaceStructureAt(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        GameObject buildingPrefab = this.structureRepo.GetBuildingPrefab(structureName, structureType);
        Vector3 gridPosition = grid.GridPosCalculator(inputPosition);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(gridPosition, grid, buildingPrefab);
            
        }
    }

    internal void RemoveBuildingAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.GridPosCalculator(inputPosition);

        if (grid.IsCellTaken(gridPosition))
        {

            placementManager.RemoveBuilding(gridPosition, grid);
        }
    }
}


