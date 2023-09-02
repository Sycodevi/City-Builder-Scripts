using UnityEngine;

public class RoadStructureScript
{
    public RotationValue RoadPrefabRotation { get; set; }
    public GameObject RoadPrefab { get; set; }
    public RoadStructureScript(GameObject roadPrefab, RotationValue roadPrefabRotation)
    {
        this.RoadPrefabRotation = roadPrefabRotation;
        this.RoadPrefab = roadPrefab;
    }
}