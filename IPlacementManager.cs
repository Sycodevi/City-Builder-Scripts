using System.Collections.Generic;
using UnityEngine;

public interface IPlacementManager
{
    void CancelPlacement(IEnumerable<GameObject> structureCollection);
    void ConfirmPlacement(IEnumerable<GameObject> structureCollection);
    GameObject CreateGhostStructure(Vector3 gridPosition, GameObject buildingPrefab, Quaternion quaternion);
    void RemoveSingleStructure(GameObject structure);
    void ResetBuildingLooks(GameObject structure);
    void setBuildingForRemoval(GameObject structureToRemove);
    void PreparePlacementManager(WorldManager worldManager);
    GameObject PlaceStructureOnMap(Vector3 gridPosition, GameObject buildingPrefab, Quaternion quaternion);
    GameObject MoveStructureOnMap(Vector3Int positionToPlaceStructure, GameObject gameObjectToReuse, GameObject prefab, Quaternion quaternion);
}