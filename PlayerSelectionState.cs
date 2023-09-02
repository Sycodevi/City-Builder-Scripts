using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSelectionState : PlayerState
{
    CameraMovement cameraman;
    BuildingManager buildingManager;
    Vector3? previousPosition;
    public PlayerSelectionState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager) 
    {
       // this.cameraman = Cameraman;
       this.buildingManager = buildingManager;
    }
    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        StructureBase data = buildingManager.GetStructureDataFromPosition(position);
        if (data)
        {
            UpdateStructureInfoPanel(data);
            previousPosition = position;
        }
        else
        {
            this.gameManager.uiController.HideStructureInfo();
            data = null;
            previousPosition = null;
        }
        return;
    }

    private void UpdateStructureInfoPanel(StructureBase data)
    {
        Type dataType = data.GetType();
        if (dataType == typeof(SingleFacilityObj))
        {
            this.gameManager.uiController.DisplayFacilityInfo((SingleFacilityObj)data);
        }
        else if (dataType == typeof(ZoneStructureObj))
        {
            this.gameManager.uiController.DisplayZoneInfo((ZoneStructureObj)data);
        }
        else
        {
            this.gameManager.uiController.DisplayBasicStructureInfo(data);
        }
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnCancel()
    {
        return;
    }
    public override void EnterState(string variable)
    {
        base.EnterState(variable);
        if (this.gameManager.uiController.GetStructureInfoVisibility())
        {
            StructureBase data = buildingManager.GetStructureDataFromPosition(previousPosition.Value);
            if(data!= null)
            {
                UpdateStructureInfoPanel(data);
            }
            else
            {
                this.gameManager.uiController.HideStructureInfo();
                previousPosition= null;
            }
            
        }
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
