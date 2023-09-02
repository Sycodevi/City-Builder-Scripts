using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureEconomyManager
{
    private static void PrepareNewStructure(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetStructureDataOnGrid(gridPosition);
        var structuresAroundThis = grid.GetStructureDataInRange(gridPosition, structureData.structureRange);

        structureData.PrepareStructure(structuresAroundThis);
    }

    public static void PrepareZoneStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareNewStructure(gridPosition, grid);
        ZoneStructureObj zoneData = (ZoneStructureObj)grid.GetStructureDataOnGrid(gridPosition);
        if (DoesStructureRequireMoreResource(zoneData))
        {
            var structureAroundPosition = grid.GetStructurePositionInRange(gridPosition, zoneData.maxFacilitySearchRange);
            foreach (var structurePositionNearby in structureAroundPosition)
            {
                var data = grid.GetStructureDataOnGrid(structurePositionNearby);
                if(data.GetType() == typeof(SingleFacilityObj))
                {
                    SingleFacilityObj facility = (SingleFacilityObj)data;
                    if((facility.facilityType == facilityType.Power && zoneData.hasPower() == false && zoneData.requirePower) || 
                        (facility.facilityType == facilityType.Water && zoneData.hasWater() == false && zoneData.requireWater))
                    {
                        if(grid.ArePositionsInRange(gridPosition, structurePositionNearby, facility.singleStructureRange))
                        {
                            if (facility.IsFull() == false)
                            {
                                facility.AddClients(new StructureBase[] { zoneData });
                                if(DoesStructureRequireMoreResource(zoneData) == false)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static bool DoesStructureRequireMoreResource(ZoneStructureObj zoneData)
    {
        return (zoneData.requirePower && zoneData.hasPower() == false) || (zoneData.requireWater && zoneData.hasWater() == false );   
    }

    public static void PrepareRoadStructure(Vector3Int gridPosition, GridStructure grid)
    {
        RoadStructureObj roadData = (RoadStructureObj)grid.GetStructureDataOnGrid(gridPosition);
        var structuresAroundRoad = grid.GetStructureDataInRange(gridPosition, roadData.structureRange);
        roadData.PrepareRoad(structuresAroundRoad);
    }
    public static void PrepareFacilityStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareNewStructure(gridPosition, grid);
        SingleFacilityObj facilityData = (SingleFacilityObj)grid.GetStructureDataOnGrid(gridPosition);
        var structuresAroundFacility = grid.GetStructureDataInRange(gridPosition, facilityData.singleStructureRange);
        facilityData.AddClients(structuresAroundFacility);
    }
    public static IEnumerable<StructureBase> PrepareFacilityRemoval(Vector3Int gridPosition, GridStructure grid)
    {
        SingleFacilityObj facilityObj = (SingleFacilityObj)grid.GetStructureDataOnGrid(gridPosition);
        return facilityObj.PrepareForRemoval();
    }
    public static IEnumerable<StructureBase> PrepareRoadRemoval(Vector3Int gridPosition, GridStructure grid)
    {
        RoadStructureObj roadData = (RoadStructureObj)grid.GetStructureDataOnGrid(gridPosition);
        var structureAroundRoad = grid.GetStructureDataInRange(gridPosition, roadData.structureRange);
        return roadData.prepareRoadRemoval(structureAroundRoad);
    }
    public static void PrepareStructureForRemoval(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetStructureDataOnGrid(gridPosition);
        structureData.PrepareForRemoval();
    }
    public static void StructureLogic(Type structureType, Vector3Int gridPosition, GridStructure grid)
    {
        if (structureType == typeof(ZoneStructureObj))
        {
            PrepareZoneStructure(gridPosition, grid);
        }
        else if (structureType == typeof(RoadStructureObj))
        {
            PrepareRoadStructure(gridPosition, grid);
        }
        else if (structureType == typeof(SingleFacilityObj))
        {
            PrepareFacilityStructure(gridPosition, grid);
        }
    }
    public static void StructureRemovalLogic(Type structureType, Vector3Int gridPosition, GridStructure grid)
    {
        if (structureType == typeof(ZoneStructureObj))
        {
            PrepareStructureForRemoval(gridPosition, grid);
        }
        else if (structureType == typeof(RoadStructureObj))
        {
            PrepareRoadRemoval(gridPosition, grid);
        }
        else if (structureType == typeof(SingleFacilityObj))
        {
            PrepareFacilityRemoval(gridPosition, grid);
        }
    }
}
