using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour
{
    private Action<string> onBuildHandler;
    private Action<string> onBuildSingleStructureHandler;
    private Action<string> onBuildRoadHandler;
    private Action onCancelHandler;
    private Action onDemolishHandler;
    private Action onConfirmActionHandler;

    public StructureRepo structureRepo;
    public Button buildResidentialbtn;
    public Button cancelbtn;
    public Button ConfirmActionBtn;
    public GameObject cancelPanel;

    public GameObject PlaceAndDemolishPanel;
    public Button openBuildMenuBtn;
    public Button demolishbtn;

    public GameObject Residential;
    public GameObject Commercial;
    public GameObject Industrial;
    public GameObject Service;
    public GameObject Road;
    public Button closeBuildBtn;

    public GameObject buildButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        cancelPanel.SetActive(false);
        PlaceAndDemolishPanel.SetActive(false);
        //buildResidentialbtn.onClick.AddListener(OnBuildCallBack);
        cancelbtn.onClick.AddListener(OnCancelCallBack);
        ConfirmActionBtn.onClick.AddListener(onConfirmActionCallback);
        openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        demolishbtn.onClick.AddListener(onDemolish);
        closeBuildBtn.onClick.AddListener(OnCloseMenuHandler);
    }

    private void onConfirmActionCallback()
    {
        cancelPanel.SetActive(false);
        onConfirmActionHandler?.Invoke();
    }

    private void OnCloseMenuHandler()
    {
        PlaceAndDemolishPanel.SetActive(false);
    }

    private void onDemolish()
    {
        onDemolishHandler?.Invoke();
        cancelPanel.SetActive(true);
        PlaceAndDemolishPanel.SetActive(false);
    }

    private void OnOpenBuildMenu()
    {
        PlaceAndDemolishPanel.SetActive(true);
        PrepareBuildMenu();
    }
    private void PrepareBuildMenu()
    {
        
        CreateButtonsInPanel(Residential.transform, structureRepo.GetResidentialZoneNames(), OnBuildCallBack);
        CreateButtonsInPanel(Commercial.transform, structureRepo.GetCommercialZoneNames(), OnBuildCallBack); 
        CreateButtonsInPanel(Industrial.transform, structureRepo.GetSingleStructureNames(), OnBuildSingleStructureCallBack);
        CreateButtonsInPanel(Service.transform, structureRepo.GetServiceZoneNames(), OnBuildCallBack);
        CreateButtonsInPanel(Road.transform, new List<string>() { structureRepo.GetRoadStructureName() },OnBuildRoadCallBack );
    }

    private void CreateButtonsInPanel(Transform Categories, List<string> dataToShow, Action<string> callback)
    {
        if (dataToShow.Count > Categories.childCount)
        {
            int quantityDifference = dataToShow.Count - Categories.childCount;
            for (int i = 0; i < quantityDifference; i++)
            {
                Instantiate(buildButtonPrefab, Categories);
            }
        }
        for(int i = 0; i < Categories.childCount; i++)
        {
            var button = Categories.GetChild(i).GetComponent<Button>();
            if (button != null)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = dataToShow[i];
                button.onClick.AddListener(()=> callback(button.name));
            }
        }

        //foreach (Transform child in Categories)
        //{
        //    var button = child.GetComponent<Button>();
        //    if(button != null)
        //    {
        //        button.onClick.RemoveAllListeners();
        //        button.onClick.AddListener(OnBuildCallBack);
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBuildCallBack(string StructureName)
    {
        uiForBuilding();
        onBuildHandler?.Invoke(StructureName);
    }
    private void OnBuildRoadCallBack(string StructureName)
    {
        uiForBuilding();
        onBuildRoadHandler?.Invoke(StructureName);
    }
    private void OnBuildSingleStructureCallBack(string StructureName)
    {
        uiForBuilding();
        onBuildSingleStructureHandler?.Invoke(StructureName);
    }

    private void uiForBuilding()
    {
        cancelPanel.SetActive(true);
        PlaceAndDemolishPanel.SetActive(false);
    }

    private void OnCancelCallBack()
    {
        cancelPanel.SetActive(false);
        onCancelHandler?.Invoke();
    }

    public void AddListenerOnBuildEvent(Action<string> listener)
    {
        onBuildHandler += listener;
    }
    public void RemoveListenerOnBuildEvent(Action<string> listener)
    {
        onBuildHandler -= listener;
    }
    public void AddListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        onBuildSingleStructureHandler += listener;
    }
    public void RemoveListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        onBuildSingleStructureHandler -= listener;
    }
    public void AddListenerOnBuildRoadEvent(Action<string> listener)
    {
        onBuildRoadHandler += listener;
    }
    public void RemoveListenerOnBuildRoadEvent(Action<string> listener)
    {
        onBuildRoadHandler -= listener;
    }
    public void AddListenerOnCancelEvent(Action listener)
    {
        onCancelHandler += listener;
    }
    public void RemoveListenerOnCancelEvent(Action listener)
    {
        onCancelHandler -= listener;
    }
    public void AddListenerOnConfirmActionEvent(Action listener)
    {
        onConfirmActionHandler += listener;
    }
    public void RemoveListenerOnConfirmActionEvent(Action listener)
    {
        onConfirmActionHandler -= listener;
    }
    public void AddListenerOnDemolishEvent(Action listener)
    {
        onDemolishHandler += listener;
    }
    public void RemoveListenerOnDemolishEvent(Action listener)
    {
        onDemolishHandler -= listener;
    }
}
