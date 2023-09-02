using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.Audio;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;

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
    public Button rotateBtn;
    public GameObject cancelPanel;
    public GameObject RotatePanel;

    public GameObject PlaceAndDemolishPanel;
    public Button openBuildMenuBtn;
    public Button demolishbtn;
    public GameObject TaxPanel;
    public Button TaxButton;
    public Button SettingsBtn;
    public Button ExitBtn;

    public GameObject Residential;
    public GameObject Commercial;
    public GameObject Industrial;
    public GameObject Service;
    public GameObject Road;
    public Button closeBuildBtn;
    public GameObject unlocksat2;
    public GameObject unlocksat3;
    public GameObject unlocksat4;
    public GameObject unlocksat5;
    public GameObject unlocksat6;
    public GameObject unlocksat7;
    public GameObject unlocksat8;
    public GameObject unlocksat9;
    public GameObject unlocksat10;
    public GameObject unlocksat11;
    public GameObject UnlocksBuildingMenu;
    public GameObject UnlocksResidential;
    public GameObject UnlocksCommerical;
    public GameObject UnlocksService;
    public GameObject OpenSettingsMenu;

    public UIInfoPanelScript buildButtonPrefab;
    public ObjectDetection objectDetection;
    public CarSpawner carSpawner;

    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI populationValue;
    public TextMeshProUGUI likesValue;
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI soundValueText;

    public UIInfoPanelScript structurePanel;

    public Button PlusButton;
    public Button MinusButton;

    public Button PlusSoundButton;
    public Button MinusSoundButton;

    public TextMeshProUGUI TaxAmountText;
    public GameObject AudioScriptGameObj;

    public NavMeshSurface surface;

    [SerializeField]
    public int moneyMultiplier = 0;
    public float soundText = 0;

    public int nextLevelUpAt;
    public int Level = 0;

    // Start is called before the first frame update
    void Start()
    {
        OpenSettingsMenu.SetActive(false);
        Unlocks();
        TaxPanel.SetActive(false);
        TaxAmountText.text = moneyMultiplier + "%";
        soundValueText.text = soundText + "";
        PlusButton.onClick.AddListener(onClickPlusButton);
        MinusButton.onClick.AddListener(onClickMinusButton);
        nextLevelUpAt = -40;
        cancelPanel.SetActive(false);
        RotatePanel.SetActive(false);
        PlaceAndDemolishPanel.SetActive(false);
        UnlocksBuildingMenu.SetActive(false);
        //buildResidentialbtn.onClick.AddListener(OnBuildCallBack);
        cancelbtn.onClick.AddListener(OnCancelCallBack);
        ConfirmActionBtn.onClick.AddListener(onConfirmActionCallback);
        openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        demolishbtn.onClick.AddListener(onDemolish);
        rotateBtn.onClick.AddListener(onRotate);
        closeBuildBtn.onClick.AddListener(OnCloseMenuHandler);
        TaxButton.onClick.AddListener(onClickTax);
        SettingsBtn.onClick.AddListener(SettingsMenu);
        ExitBtn.onClick.AddListener(ExitApplication);
        PlusSoundButton.onClick.AddListener(onClickPlusSoundButton);
        MinusSoundButton.onClick.AddListener(onClickMinusSoundButton);
    }
    public void ExitApplication()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void SettingsMenu()
    {
        AudioScript.Instance.PlayButtonClickedSFX();

        if (OpenSettingsMenu.activeInHierarchy == true)
        {
            OpenSettingsMenu.SetActive(false);
        }
        else
        {
            OpenSettingsMenu.SetActive(true);
        }
    }
    public void Unlocks()
    {
        switch(Level)
        {
            case 1:
                unlocksat2.SetActive(true);
                unlocksat3.SetActive(true);
                unlocksat4.SetActive(true);
                unlocksat5.SetActive(true);
                unlocksat6.SetActive(true);
                unlocksat7.SetActive(true);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 2:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(true);
                unlocksat4.SetActive(true);
                unlocksat5.SetActive(true);
                unlocksat6.SetActive(true);
                unlocksat7.SetActive(true);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 3:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(true);
                unlocksat5.SetActive(true);
                unlocksat6.SetActive(true);
                unlocksat7.SetActive(true);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 4:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(true);
                unlocksat6.SetActive(true);
                unlocksat7.SetActive(true);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 5:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(true);
                unlocksat7.SetActive(true);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 6:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(true);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 7:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(false);
                unlocksat8.SetActive(true);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 8:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(false);
                unlocksat8.SetActive(false);
                unlocksat9.SetActive(true);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 9:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(false);
                unlocksat8.SetActive(false);
                unlocksat9.SetActive(false);
                unlocksat10.SetActive(true);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 10:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(false);
                unlocksat8.SetActive(false);
                unlocksat9.SetActive(false);
                unlocksat10.SetActive(false);
                unlocksat11.SetActive(true);
                Debug.Log("Level " + Level);
                break;
            case 11:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(false);
                unlocksat8.SetActive(false);
                unlocksat9.SetActive(false);
                unlocksat10.SetActive(false);
                unlocksat11.SetActive(false);
                Debug.Log("Level " + Level);
                break;
            case 12:
                unlocksat2.SetActive(false);
                unlocksat3.SetActive(false);
                unlocksat4.SetActive(false);
                unlocksat5.SetActive(false);
                unlocksat6.SetActive(false);
                unlocksat7.SetActive(false);
                unlocksat8.SetActive(false);
                unlocksat9.SetActive(false);
                unlocksat10.SetActive(false);
                unlocksat11.SetActive(false);
                Debug.Log("Level " + Level);
                break;
            default: break;
        }
    }

    private void onClickTax()
    {
        AudioScript.Instance.PlayButtonClickedSFX();

        if (TaxPanel.activeSelf == false)
        {
            TaxPanel.SetActive(true);
        }
        else
        {
            TaxPanel.SetActive(false);
        }
    }

    private void onClickMinusButton()
    {
        if (moneyMultiplier == 0)
        {
            return;
        }
        else
        {
            moneyMultiplier -= 10;
            TaxAmountText.text = moneyMultiplier + "%";
        }
    }

    public void onClickPlusButton()
    {
        if (moneyMultiplier == 100)
        {
            return;
        }
        else
        {
            moneyMultiplier += 10;
            TaxAmountText.text = moneyMultiplier + "%";
        }
    }
    private void onClickMinusSoundButton()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        //AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume -= .10f;
        if (AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume == 0f)
        {
            return;
        }
        else
        {
            AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume -= 0.10f;
            soundValueText.text = (Mathf.RoundToInt(AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume * 100) + "%");
        }
    }

    public void onClickPlusSoundButton()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        if (AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume == 1f)
        {
            return;
        }
        else
        {
            AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume += 0.10f;
            soundValueText.text = (Mathf.RoundToInt(AudioScriptGameObj.GetComponentInChildren<AudioSource>().volume * 100) + "%");
        }
    }

    public int rotateVal = 0;
    private void onRotate()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        rotateVal++;
        Debug.Log(rotateVal);
        switch (rotateVal)
        {
            case 0:
                rotateBtn.image.transform.rotation = Quaternion.Euler(0, 0, 225);
                break;
            case 1:
                rotateBtn.image.transform.rotation = Quaternion.Euler(0, 0, 135);
                break;
            case 2:
                rotateBtn.image.transform.rotation = Quaternion.Euler(0, 0, 45);
                break;
            case 3:
                rotateBtn.image.transform.rotation = Quaternion.Euler(0, 0, -45);
                break;
            default:
                rotateVal = 0; goto case 0;
        }

    }
    private void onConfirmActionCallback()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        cancelPanel.SetActive(false);
        RotatePanel.SetActive(false);
        objectDetection.isConfirmationPressed = true;
        surface.BuildNavMesh();
        carSpawner.HouseAndCarCountCheck();
        onConfirmActionHandler?.Invoke();
    }

    private void OnCloseMenuHandler()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        PlaceAndDemolishPanel.SetActive(false);
        UnlocksBuildingMenu.SetActive(false);
    }

    private void onDemolish()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        onDemolishHandler?.Invoke();
        cancelPanel.SetActive(true);
        PlaceAndDemolishPanel.SetActive(false);
        UnlocksBuildingMenu.SetActive(false);
    }

    private void OnOpenBuildMenu()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        PlaceAndDemolishPanel.SetActive(true);
        UnlocksBuildingMenu.SetActive(true);
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
    public void UpdateLevel()
    {
        int numberOfLikes = Convert.ToInt32(likesValue.text);

        if(numberOfLikes > nextLevelUpAt)
        {
            Level += 1;
            if(Level <= 5)
            {
                nextLevelUpAt += 50;
            }
            else
            {
                nextLevelUpAt += 100;
            }
            
            LevelValue.text = "Level " + Level;
        }
        Unlocks();
    }
    public void SetLevel(int value)
    {
        Level = value;
        LevelValue.text = "Level " + Level;
        Unlocks();
        Debug.Log("Level set to " + Level);
    }
    public void DisplayBasicStructureInfo(StructureBase data)
    {
        structurePanel.DisplayBasicStructureInfo(data);
    }
    public void DisplayZoneInfo(ZoneStructureObj data)
    {
        structurePanel.DisplayZoneInfo(data);
    }
    public void DisplayFacilityInfo(SingleFacilityObj data)
    {
        structurePanel.DisplayFactoryInfo(data);
    }

    private void OnBuildCallBack(string StructureName)
    {
        uiForBuilding();
        AudioScript.Instance.PlayButtonClickedSFX();
        onBuildHandler?.Invoke(StructureName);
    }
    private void OnBuildRoadCallBack(string StructureName)
    {
        uiForBuilding();
        AudioScript.Instance.PlayButtonClickedSFX();
        onBuildRoadHandler?.Invoke(StructureName);
    }
    private void OnBuildSingleStructureCallBack(string StructureName)
    {
        uiForBuilding();
        AudioScript.Instance.PlayButtonClickedSFX();
        onBuildSingleStructureHandler?.Invoke(StructureName);
    }

    private void uiForBuilding()
    {

        RotatePanel.SetActive(true);
        cancelPanel.SetActive(true);
        PlaceAndDemolishPanel.SetActive(false);
        UnlocksBuildingMenu.SetActive(false);
        UnlocksResidential.SetActive(false);
        UnlocksCommerical.SetActive(false);
        UnlocksService.SetActive(false);
    }

    private void OnCancelCallBack()
    {
        AudioScript.Instance.PlayButtonClickedSFX();
        RotatePanel.SetActive(false);
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

    public void SetMoneyValue(int money)
    {
        moneyValue.text = money + "";
    }

    public void setPopulationValue(int population)
    {
        populationValue.text = population + "";
    }
    public void setLikesValue(int likes)
    {
        likesValue.text = likes + "";
    }

    public void HideStructureInfo()
    {
        structurePanel.Hide();
    }
    public bool GetStructureInfoVisibility()
    {
        return structurePanel.gameObject.activeSelf;
    }
}

enum UiRotate
{
    Rotate0,
    Rotate90,
    Rotate180,
    Rotate270
}
