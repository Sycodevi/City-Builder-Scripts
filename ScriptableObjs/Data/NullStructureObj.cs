using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullStructureObj : StructureBase
{
    private void OnEnable()
    {
        buildingName= "Null Object";
        prefab = null;
        placementCost = 0;
        upkeepCost = 0;
        income = 0;
        requireRoad = false;
        requirePower = false;
        requireWater = false;
    }
}
