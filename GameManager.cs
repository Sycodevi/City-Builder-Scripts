using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public StructureRepo structureRepo;
    public IInputManager inputManager;
    public int width, lenght;
    public UIController uiController;
    private GridStructure grid;
    private int cellSize = 3;
    public CameraMovement Cameraman;
    private BuildingManager buildingManager;
    public LayerMask inputMask;

    private PlayerState state;

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerRemoveBuildingState demolishState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerBuildingState buildingState;


    public PlayerState State { get => state; }

    private void Awake()
    {
        StatePreparations();

#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }

    private void StatePreparations()
    {
        buildingManager = new BuildingManager(cellSize, width, lenght, placementManager, structureRepo);
        selectionState = new PlayerSelectionState(this, Cameraman);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingState = new PlayerBuildingState(this, buildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
    }


    // Start is called before the first frame update
    void Start()
    {
        inputManager.MouseInputMask = inputMask;
        Cameraman.SetCameraBounds(0, width, 0, lenght);
        //inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();

        InputListeners();
        UiControllerListeners();
    }

    private void UiControllerListeners()
    {
        uiController.AddListenerOnBuildEvent((structureName)=>state.onBuild(structureName));
        uiController.AddListenerOnBuildSingleStructureEvent((structureName) => state.onBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadEvent((structureName)=> state.onBuildRoad(structureName));
        uiController.AddListenerOnCancelEvent(()=>state.OnCancel());
        uiController.AddListenerOnDemolishEvent(()=>state.OnDemolishAction());
        uiController.AddListenerOnConfirmActionEvent(()=>state.OnConfirmAction());
    }

    private void InputListeners()
    {
        inputManager.AddListenerOnPointerDownEvent((position)=>state.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondChangeEvent((position)=>state.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(()=>state.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position)=>state.OnInputPointerChange(position));
    }

    //private void EnableDemolishMode()
    //{
    //    TransitionToState(demolishState, null);
    //}

    //private void HandlePointerChange(Vector3 position)
    //{
    //    state.OnInputPointerChange(position);
    //}

    //private void HandleInputMouseDragStop()
    //{
    //      state.OnInputPanUp();
    //}

    //private void HandleInputMouseDrag(Vector3 position)
    //{
    //    state.OnInputPanChange(position);
    //}

    //private void HandleInput(Vector3 position)
    //{
    //    state.OnInputPointerDown(position);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void EnableBuildingMode(string variable)
    //{
    //    TransitionToState(buildingSingleStructureState, variable);
    //}
    //private void DisableBuildingMode()
    //{
    //    state.OnCancel();
    //}
    public void TransitionToState(PlayerState newState, string variable)
    {
        this.state = newState;
        this.state.EnterState(variable);
    }
}
