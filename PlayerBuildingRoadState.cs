using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingRoadState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingRoadState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }
    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
    public override void onBuild(string structureName)
    {
        this.buildingManager.CancelModification();
        base.onBuild(structureName);
    }
    public override void OnDemolishAction()
    {
        this.buildingManager.CancelModification();
        base.OnDemolishAction();
    }
    public override void onBuildSingleStructure(string structureName)
    {
        this.buildingManager.CancelModification();
        base.onBuildSingleStructure(structureName);
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
        this.buildingManager.PrepareStructureForModification(position, this.structureName, StructureType.Road);
    }
}
