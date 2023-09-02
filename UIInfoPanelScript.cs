using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanelScript : MonoBehaviour
{
    public TextMeshProUGUI nameText, incomeText, upkeepText, clientText;
    public Toggle powerToggle, waterToggle, roadToggle;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void DisplayBasicStructureInfo(StructureBase data)
    {
        Show();
        HideElement(clientText.gameObject);
        HideElement(powerToggle.gameObject);
        HideElement(waterToggle.gameObject);
        HideElement(roadToggle.gameObject);
        SetText(nameText, data.buildingName);
        SetText(incomeText, data.GetIncome()+"");
        SetText(upkeepText, data.upkeepCost + "");
    }
    public void DisplayZoneInfo(ZoneStructureObj data)
    {
        Show();
        HideElement(clientText.gameObject);
        SetText(nameText, data.buildingName);
        SetText(incomeText, data.GetIncome() + "");
        SetText(upkeepText, data.upkeepCost + "");

        if(data.requirePower)
        {
            SetToggle(powerToggle, data.hasPower());
        }
        else
        {
            HideElement(powerToggle.gameObject);
        }
        if (data.requireWater)
        {
            SetToggle(waterToggle, data.hasWater());
        }
        else
        {
            HideElement(waterToggle.gameObject);
        }
        if (data.requireRoad)
        {
            SetToggle(roadToggle, data.hasRoad());
        }
        else
        {
            HideElement(roadToggle.gameObject);
        }
    }
    public void DisplayFactoryInfo(SingleFacilityObj data)
    {
        Show();
        SetText(nameText, data.buildingName);
        SetText(incomeText, data.GetIncome() + "");
        SetText(upkeepText, data.upkeepCost + "");

        if (data.requirePower)
        {
            SetToggle(powerToggle, data.hasPower());
        }
        else
        {
            HideElement(powerToggle.gameObject);
        }
        if (data.requireWater)
        {
            SetToggle(waterToggle, data.hasWater());
        }
        else
        {
            HideElement(waterToggle.gameObject);
        }
        if (data.requireRoad)
        {
            SetToggle(roadToggle, data.hasRoad());
        }
        else
        {
            HideElement(roadToggle.gameObject);
        }
        SetText(clientText, data.GetNumberofCustomers()+"/"+data.maxCustomers);
    }
    private void HideElement(GameObject element)
    {
        element.transform.parent.gameObject.SetActive(false);
    }
    private void ShowElement(GameObject element)
    {
        element.transform.parent.gameObject.SetActive(true);
    }
    private void SetText(TextMeshProUGUI element, string value)
    {
        ShowElement(element.gameObject);
        element.text = value;
    }
    private void SetToggle(Toggle element, bool value)
    {
        ShowElement(element.gameObject);
        element.isOn = value;
    }
}
