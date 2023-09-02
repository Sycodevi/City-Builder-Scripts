using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected GameManager gameManager;
    protected CameraMovement cameraman;

    public PlayerState(GameManager gameManager)
	{
		this.gameManager = gameManager;
        cameraman = gameManager.Cameraman;
    }
    public virtual void OnConfirmAction()
    {
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
	public virtual void OnInputPointerDown(Vector3 position)
	{

	}
	public virtual void OnInputPointerChange(Vector3 position)
	{

	}
	public virtual void OnInputPointerUp()
	{

	}
	public virtual void EnterState(string variable)
	{

	}
    public abstract void OnCancel();
    public virtual void onBuild(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingState, structureName);
    }
    public virtual void onBuildSingleStructure(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingSingleStructureState, structureName);
    }
    public virtual void onBuildRoad(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingRoadState, structureName);
    }
    public virtual void OnDemolishAction()
    {
        this.gameManager.TransitionToState(this.gameManager.demolishState, null);
    }
    public virtual void OnInputPanChange(Vector3 dragposition)
    {
        cameraman.MoveCamera(dragposition);
    }

    public virtual void OnInputPanUp()
    {
        cameraman.StopCameraMovement();
    }

}
