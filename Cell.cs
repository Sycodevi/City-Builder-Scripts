using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cell
{
   GameObject structureModel = null;
    List<GameObject> grasslandList = new List<GameObject>();
    StructureBase structureData;
    bool isTaken = false;

    public bool IsTaken { get => isTaken; }

    public void SetConstruction(GameObject structureModel, StructureBase structureData)
    {
        if (structureModel == null)
            return;
        this.structureData = structureData;
        this.structureModel = structureModel;
        this.isTaken = true;
    }

    public GameObject GetStructure()
    {
        return structureModel;
    }

    public void RemoveStructure()
    {
        structureModel= null;
        isTaken = false;
        structureData = null;
    }

    public StructureBase GetStructureData()
    {
        return structureData;
    }

    public void AddNatureObject(GameObject element)
    {
        grasslandList.Add(element);
    }
    public List<GameObject> GetNatureOnThisCell()
    {
        return grasslandList;
    }
}
