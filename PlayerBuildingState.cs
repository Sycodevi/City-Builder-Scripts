using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnCancel()
    {
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        this.structureName = structureName;
    }
    public override void OnInputPointerDown(Vector3 position)
    {
        if(structureName.Contains("Upper") || structureName.Contains("Lower") || structureName.Contains("Middle"))
        {
            this.buildingManager.PlaceStructureAt(position, structureName, StructureType.Residential);
            Debug.Log("Type = Residential");
        }
        else if (structureName.Contains("Shop") || structureName.Contains("Office") || structureName.Contains("Market"))
        {
            this.buildingManager.PlaceStructureAt(position, structureName, StructureType.Commercial);
            Debug.Log("Type = Commercial");
        }
        else if (structureName.Contains("Fire") || structureName.Contains("Police") || structureName.Contains("Sports"))
        {
            this.buildingManager.PlaceStructureAt(position, structureName, StructureType.Service);
            Debug.Log("Type = Service");
        }


    }

}
