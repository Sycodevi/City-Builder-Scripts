using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureBase : ScriptableObject
{
    public string buildingName;
    public GameObject prefab;
    public int placementCost;
    public int upkeepCost;

    [SerializeField]
    protected int income;

    public bool requireRoad;
    public bool requireWater;
    public bool requirePower;

    public int structureRange = 1;
    public int structureCount = 0;

    private SingleFacilityObj powerProvider = null;
    private SingleFacilityObj waterProvider = null;
    private RoadStructureObj roadProvider = null;

    public SingleFacilityObj PowerProvider { get => powerProvider; }
    public SingleFacilityObj WaterProvider { get => waterProvider; }
    public RoadStructureObj RoadProvider { get => roadProvider; }

    public ZoneStructureObj zoneStructureObj;

    public virtual int GetIncome()
    {
        return income;
    }
    public bool hasPower()
    {
        return powerProvider != null;
    }

    public bool hasWater()
    {
        return waterProvider != null;
    }

    public bool hasRoad()
    {
        return roadProvider != null;
    }

    public void prepareStructure(IEnumerable<StructureBase> structureInRange)
    {
        AddRoadProvider(structureInRange);
    }

    public bool AddPowerProvider(SingleFacilityObj facility)
    {
        if (powerProvider == null)
        {
            powerProvider = facility;
            return true;
        }
        return false;
    }

    public bool AddWaterProvider(SingleFacilityObj facility)
    {
        if (waterProvider == null)
        {
            waterProvider = facility;
            return true;
        }
        return false;
    }

    public virtual IEnumerable<StructureBase> PrepareForRemoval()
    {
        if(powerProvider != null)
        {
            powerProvider.RemoveClient(this);
        }
        if (waterProvider != null)
        {
            waterProvider.RemoveClient(this);
        }
        return null;
    }

    public void AddRoadProvider(IEnumerable<StructureBase> structureInRange)
    {
        if(roadProvider != null)
        {
            return;
        }
        foreach (var nearbyStructure in structureInRange)
        {
            if(nearbyStructure.GetType() == typeof(RoadStructureObj))
            {
                roadProvider = (RoadStructureObj)nearbyStructure;
                return;
            }
        }
    }

    internal void removeWaterFacility()
    {
        waterProvider = null;
    }

    internal void removePowerFacility()
    {
        powerProvider = null;
    }

    public void PrepareStructure(IEnumerable<StructureBase> structureInRange)
    {
        AddRoadProvider(structureInRange);
    }

    internal void removeRoadProvider()
    {
        roadProvider = null;
    }
}
