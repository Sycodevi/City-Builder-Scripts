using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionState : PlayerState
{
    CameraMovement cameraman;
    public PlayerSelectionState(GameManager gameManager, CameraMovement Cameraman) : base(gameManager) 
    {
        this.cameraman = Cameraman;
    }
    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnCancel()
    {
        return;
    }
    //public override void onBuild(string structurename)
    //{
    //    this.gameManager.TransitionToState(this.gameManager.buildingState, structurename);
    //}
    //public override void onBuildSingleStructure(string structurename)
    //{
    //    this.gameManager.TransitionToState(this.gameManager.buildingSingleStructureState, structurename);
    //}
    //public override void onBuildRoad(string structurename)
    //{
    //    this.gameManager.TransitionToState(this.gameManager.buildingRoadState, structurename);
    //}
    //public override void OnDemolishAction()
    //{
    //    this.gameManager.TransitionToState(this.gameManager.demolishState, null);

    //}
}
