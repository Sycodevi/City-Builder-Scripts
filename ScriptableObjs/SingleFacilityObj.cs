using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Facility", menuName = "CityBuilder3D/StructureData/Facility")]
public class SingleFacilityObj : SingleStructureObj
{
    public int maxCustomers;
    public int upkeepPerCustomer;
    private HashSet<StructureBase> customers = new HashSet<StructureBase>();
    public facilityType facilityType = facilityType.None;

    public void RemoveClient(StructureBase clientStructure)
    {
        if (customers.Contains(clientStructure))
        {
            if(facilityType == facilityType.Water)
            {
                clientStructure.removeWaterFacility();
            }
            if(facilityType == facilityType.Power)
            {
                clientStructure.removePowerFacility();
            }
            customers.Remove(clientStructure);

        }
    }
    public override int GetIncome()
    {
        return customers.Count * income;
    }

    public int GetNumberofCustomers()
    {
        return customers.Count;
    }

    public void AddClients(IEnumerable<StructureBase> structuresAroundFacility)
    {
        foreach (var nearbyStructure in structuresAroundFacility)
        {
            if(maxCustomers > customers.Count && nearbyStructure != this)
            {
                if(facilityType == facilityType.Power && nearbyStructure.requireWater)
                {
                    if (nearbyStructure.AddPowerProvider(this))
                    {
                        customers.Add(nearbyStructure);
                    }
                }
                if(facilityType == facilityType.Water && nearbyStructure.requireWater)
                {
                    if (nearbyStructure.AddWaterProvider(this))
                    {
                        customers.Add(nearbyStructure);
                    }
                }
            }
        }
    }
    public override IEnumerable<StructureBase> PrepareForRemoval()
    {
        base.PrepareForRemoval();
        List<StructureBase> tempList = new List<StructureBase>(customers);
        foreach (var clientStructure in tempList)
        {
            RemoveClient(clientStructure);
        }
        return tempList;
    }

    internal bool IsFull()
    {
        return GetNumberofCustomers() >= maxCustomers;
    }
}

public enum facilityType
{
    Power,
    Water,
    None
}
