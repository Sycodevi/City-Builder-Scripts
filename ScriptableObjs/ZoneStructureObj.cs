using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zone Structure", menuName = "CityBuilder3D/StructureData/ZoneStructure")]
public class ZoneStructureObj : StructureBase
{
    public GameObject[] prefabVariants;
    public bool Upgradable;
    public UpgradeType[] availableUpgrades;
    public ZoneType zoneType;
    public int maxFacilitySearchRange;
}

[System.Serializable]
public struct UpgradeType
{
    public GameObject[] prefabVariants;
    public int Happiness;
    public int NewIncome;
    public int NewUpkeep;
}

public enum ZoneType
{
    Residential,
    Commercial,
    Industrial,
    Service,
    None
}
