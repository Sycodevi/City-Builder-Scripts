using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collection", menuName = "CityBuilder3D/CollectionObj")]
public class CollectionObj : ScriptableObject
{
    public RoadStructureObj roadStructure;
    public List<SingleStructureObj> singleStructureList;
    public List<ZoneStructureObj> ResidentialZoneList;
    public List<ZoneStructureObj> CommercialZoneList;
    public List<ZoneStructureObj> ServiceZoneList;
}
