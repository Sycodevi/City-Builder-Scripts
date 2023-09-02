using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Road Structure", menuName = "CityBuilder3D/StructureData/Road Structure")]
public class RoadStructureObj : StructureBase
{
    
    public GameObject cornerPrefab;
    public GameObject threewayPrefab;
    public GameObject fourwayPrefab;
    public RotationValue prefabRotationValue = RotationValue.Rotate0;

    public void PrepareRoad(IEnumerable<StructureBase> structuresAround)
    {
        foreach (var nearbyStructures in structuresAround)
        {
            nearbyStructures.PrepareStructure(new StructureBase[] { this });
        }
    }
    public IEnumerable<StructureBase> prepareRoadRemoval(IEnumerable<StructureBase> structuresAround)
    {
        List<StructureBase> listToReturn = new List<StructureBase>();
        foreach (var nearbyStructure in structuresAround)
        {
            if(nearbyStructure.RoadProvider == this)
            {
                nearbyStructure.removeRoadProvider();
                listToReturn.Add(nearbyStructure);
            }
        }
        return listToReturn;
    }
}

public enum RotationValue
{
    Rotate0,
    Rotate90,
    Rotate180,
    Rotate270
}