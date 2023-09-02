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
        this.buildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
    public override void onBuildRoad(string structureName)
    {
        this.buildingManager.CancelModification();
        base.onBuildRoad(structureName);
    }
    public override void onBuildSingleStructure(string structureName)
    {
        this.buildingManager.CancelModification();
        base.onBuildSingleStructure(structureName);
    }
    public override void OnDemolishAction()
    {
        this.buildingManager.CancelModification();
        base.OnDemolishAction();
    }
    public override void OnConfirmAction()
    {
        AudioScript.Instance.PlaceBuildingButtonClickedSFX();

        this.buildingManager.ConfirmModification();
        base.OnConfirmAction();
    }

    public override void EnterState(string structureName)
    {
        this.buildingManager.prepareBuildingManager(this.GetType());
        this.structureName = structureName;
    }
    public override void OnInputPointerDown(Vector3 position)
    {
        if(structureName.Contains("Upper") || structureName.Contains("Lower") || structureName.Contains("Middle"))
        {
            this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Residential);
        }
        else if (structureName.Contains("Shop") || structureName.Contains("Office") || structureName.Contains("Market"))
        {
            this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Commercial);
        }
        else if (structureName.Contains("Fire") || structureName.Contains("Police") || structureName.Contains("Sports"))
        {
            this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Service);
        }
    }
    public override void OnInputPointerChange(Vector3 position)
    {
        if (structureName.Contains("Upper") || structureName.Contains("Lower") || structureName.Contains("Middle"))
        {
            this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Residential);
        }
        else if (structureName.Contains("Shop") || structureName.Contains("Office") || structureName.Contains("Market"))
        {
            this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Commercial);
        }
        else if (structureName.Contains("Fire") || structureName.Contains("Police") || structureName.Contains("Sports"))
        {
            this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Service);
        }
    }
    public override void OnInputPointerUp()
    {
        this. buildingManager.StopContinuedPlacement();
    }

}
