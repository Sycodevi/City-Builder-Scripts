using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureBase : ScriptableObject
{
    public string buildingName;
    public GameObject prefab;
    public int placementCost;
    public int upkeepCost;
    public int income;

    public bool requireRoad;
    public bool requireWater;
    public bool requirePower;
}
