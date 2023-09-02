using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRemoveBuildingState : PlayerState
{
    BuildingManager buildingManager;
    public PlayerRemoveBuildingState(GameManager gameManager, BuildingManager buildingManager):base(gameManager)
    {
        this.buildingManager = buildingManager;
    }
    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmModification();
        AudioScript.Instance.RemoveBuildingButtonSFX();
        base.OnConfirmAction();
    }
    public override void onBuild(string structureName)
    {
        this.buildingManager.CancelModification();

        base.onBuild(structureName);
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
    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.buildingManager.PrepareBuildingForRemovalAt(position);
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void EnterState(string variable)
    {
        //this.buildingManager.CancelModification();

        this.buildingManager.prepareBuildingManager(this.GetType());

    }
}
