using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureRepo : MonoBehaviour
{
    public CollectionObj modelData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<string> GetResidentialZoneNames()
    {
        return modelData.ResidentialZoneList.Select(zone => zone.buildingName).ToList();
    }
    public List<string> GetCommercialZoneNames()
    {
        return modelData.CommercialZoneList.Select(zone => zone.buildingName).ToList();
    }
    public List<string> GetServiceZoneNames()
    {
        return modelData.ServiceZoneList.Select(zone => zone.buildingName).ToList();
    }
    public List<string> GetSingleStructureNames()
    {
        return modelData.singleStructureList.Select(facility => facility.buildingName).ToList();
    }
    public string GetRoadStructureName()
    {
        return modelData.roadStructure.buildingName;
    }

    public GameObject GetBuildingPrefab(string structureName, StructureType structureType)
    {
        GameObject structurePrefabReturn = null;   
        switch (structureType)
        {
            case StructureType.Residential:
                    structurePrefabReturn = GetResidentialBuildingPrefabByName(structureName);
                break;
            case StructureType.Commercial:
                structurePrefabReturn = GetCommercialBuildingPrefabByName(structureName);
                break;
            case StructureType.Service:
                structurePrefabReturn = GetServiceBuildingPrefabByName(structureName);
                break;
            case StructureType.SingleStructure:
                structurePrefabReturn = GetSingleStructureBuildingPrefabByName(structureName);
                break;
            case StructureType.Road:
                structurePrefabReturn = GetRoadPrefab(structureName);
                break;
            default:
                throw new Exception("No such type implemented for " + structureType);
        }
        if(structurePrefabReturn == null) 
        {
            throw new Exception("No prefab implemented for " + structureName + " in " + structureType);
        }
        return structurePrefabReturn;
    }

    private GameObject GetRoadPrefab(string structureName)
    {
        return modelData.roadStructure.prefab;
    }

    private GameObject GetSingleStructureBuildingPrefabByName(string structureName)
    {
        var structure = modelData.singleStructureList.Where(structure => structure.buildingName == structureName).FirstOrDefault();
        if(structure != null)
        {
            return structure.prefab;
        }
        return null;
    }

    private GameObject GetServiceBuildingPrefabByName(string structureName)
    {
        var structure = modelData.ServiceZoneList.Where(structure => structure.buildingName == structureName).FirstOrDefault();
        if (structure != null)
        {
            return structure.prefab;
        }
        return null;
    }

    private GameObject GetCommercialBuildingPrefabByName(string structureName)
    {
        var structure = modelData.CommercialZoneList.Where(structure => structure.buildingName == structureName).FirstOrDefault();
        if (structure != null)
        {
            return structure.prefab;
        }
        return null;
    }

    private GameObject GetResidentialBuildingPrefabByName(string structureName)
    {
        var structure = modelData.ResidentialZoneList.Where(structure => structure.buildingName == structureName).FirstOrDefault();
        if (structure != null)
        {
            return structure.prefab;
        }
        return null;
    }
}

public enum StructureType
{
    Residential,
    Commercial,
    Service,
    SingleStructure,
    Road
}