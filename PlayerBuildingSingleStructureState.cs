using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingSingleStructureState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }



    public override void OnConfirmAction()
    {
        AudioScript.Instance.PlaceBuildingButtonClickedSFX();

        this.buildingManager.ConfirmModification();
        base.OnConfirmAction();
    }
    public override void OnInputPanChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPanUp()
    {
        return;
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Debug.Log("Single");
        this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.SingleStructure);
    }

    public override void OnInputPointerUp()
    {
        return;
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
    public override void OnDemolishAction()
    {
        this.buildingManager.CancelModification();
        base.OnDemolishAction();
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        this.buildingManager.prepareBuildingManager(this.GetType());

        this.structureName = structureName;
    }
}
