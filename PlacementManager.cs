using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObjList gameObjList;
    public Transform ground;

    public void CreateBuilding(Vector3 gridPosition, GridStructure grid, GameObject buildingPrefab)
    {
        
        GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
        gameObjList.StructureIncreased();
        grid.PlaceStructureOnGrid(newStructure, gridPosition);
    }

    public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    {
        var structure = grid.GetStructureOnGrid(gridPosition);
        if(structure != null)
        {
            Debug.Log("Structure transform = " + structure.transform.position);
            Destroy(structure);
            grid.RemoveStructureOnGrid(structure.transform.position);
        }
    }
}