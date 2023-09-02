using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour, IPlacementManager
{
    public GameObjList gameObjList;
    public Transform ground;
    public Material transparentMat;
    UIController uiController;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();
    private WorldManager worldManager;
    //public void CreateBuilding(Vector3 gridPosition, GridStructure grid, GameObject buildingPrefab)
    //{
    //    GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
    //    gameObjList.StructureIncreased();
    //    grid.PlaceStructureOnGrid(newStructure, gridPosition);
    //}
    public void PreparePlacementManager(WorldManager worldManager)
    {
        this.worldManager = worldManager; 
    }
    public GameObject CreateGhostStructure(Vector3 gridPosition, GameObject buildingPrefab, Quaternion quaternion)
    {
        GameObject newStructure = PlaceStructureOnMap(gridPosition, buildingPrefab, quaternion);
        Color colorToSet = Color.cyan;
        ModifyStructurePrefabColor(newStructure, colorToSet);
        return newStructure;
    }

    public GameObject PlaceStructureOnMap(Vector3 gridPosition, GameObject buildingPrefab, Quaternion quaternion)
    {
        GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
        Vector3 rotation = Vector3.zero;
        /*switch (rotationValue)
        {
            case RotationValue.Rotate0:
                rotation = new Vector3(0, 0, 0);
                break;
            case RotationValue.Rotate90:
                rotation = new Vector3(0,90,0);
                break;
            case RotationValue.Rotate180:
                rotation = new Vector3(0, 180, 0);
                break;
            case RotationValue.Rotate270:
                rotation = new Vector3(0, 270, 0);
                break;
            default:
                break;
        }*/
        foreach (Transform child in newStructure.transform)
        {
            child.Rotate(quaternion.eulerAngles, Space.World);
        }
        return newStructure;
    }

    private void ModifyStructurePrefabColor(GameObject newStructure, Color colorToSet)
    {
        foreach (Transform child in newStructure.transform)
        {
            var renderer = child.GetComponent<MeshRenderer>();
            var roadrenderer = GetComponent<MeshRenderer>();
            if (originalMaterials.ContainsKey(child.gameObject) == false)
            {
                originalMaterials.Add(child.gameObject, renderer.materials);
            }
            Material[] materialsToSet = new Material[renderer.materials.Length];
            colorToSet.a = 0.5f;
            for (int i = 0; i < materialsToSet.Length; i++)
            {
                materialsToSet[i] = transparentMat;
                materialsToSet[i].color = colorToSet;
            }
            renderer.materials = materialsToSet;
        }
    }

    public void ConfirmPlacement(IEnumerable<GameObject> structureCollection)
    {
        foreach (var structure in structureCollection)
        {
            worldManager.DestroyEnvironmentOnLocation(structure.transform.position);
            ResetBuildingLooks(structure);

        }
        originalMaterials.Clear();
    }

    public void ResetBuildingLooks(GameObject structure)
    {
        foreach (Transform child in structure.transform)
        {
            var renderer = child.GetComponent<MeshRenderer>();
            if (originalMaterials.ContainsKey(child.gameObject))
            {
                renderer.materials = originalMaterials[child.gameObject];
            }
        }
    }

    public void CancelPlacement(IEnumerable<GameObject> structureCollection)
    {
        foreach (var structure in structureCollection)
        {
            RemoveSingleStructure(structure);
        }
        originalMaterials.Clear();
    }

    public void RemoveSingleStructure(GameObject structure)
    {
        Destroy(structure);
    }

    //public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    //{
    //    var structure = grid.GetStructureOnGrid(gridPosition);
    //    if(structure != null) 
    //    {
    //        Debug.Log("Structure transform = " + structure.transform.position);
    //        Destroy(structure);
    //        grid.RemoveStructureOnGrid(structure.transform.position);
    //    }
    //}

    public void setBuildingForRemoval(GameObject structureToRemove)
    {
        Color colorToSet = Color.red;
        ModifyStructurePrefabColor(structureToRemove, colorToSet);
    }

    public GameObject MoveStructureOnMap(Vector3Int positionToPlaceStructure, GameObject gameObjectToReuse, GameObject prefab, Quaternion quaternion)
    {
        gameObjectToReuse.transform.position = positionToPlaceStructure;
        gameObjectToReuse.transform.rotation = prefab.transform.rotation;
        for ( int i = 0; i < gameObjectToReuse.transform.childCount; i++ )
        {
            gameObjectToReuse.transform.GetChild(i).rotation = prefab.transform.GetChild(i).rotation;
        }
        return gameObjectToReuse;

    }
}