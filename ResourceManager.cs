using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    [SerializeField]
    private int startMoneyAmount = 50000;
    [SerializeField]
    private int removalPrice = 100;
    [SerializeField]
    private float moneyCalculationInterval = 5;
    MoneyHelper moneyHelper;
    PopulationScript populationScript;
    LikesScript likesScript;
    private BuildingManager buildingManager;
    public UIController uiController;

    public int StartMoneyAmount { get => startMoneyAmount; }
    public float MoneyCalculationInterval { get => moneyCalculationInterval; }

    int IResourceManager.removalPrice { get => removalPrice; }

    void Start()
    {
        moneyHelper = new MoneyHelper(startMoneyAmount);
        populationScript = new PopulationScript();
        likesScript= new LikesScript();
        UpdateUI();
    }

    public void prepareRM(BuildingManager buildingManager) //Resource Manager
    {
        this.buildingManager = buildingManager;
        InvokeRepeating("CalculateTownIncome", 0, moneyCalculationInterval);
    }


    public bool SpendMoney(int amount)
    {
        if (Purchasable(amount))
        {
            try
            {
                moneyHelper.DecreaseMoney(amount);
                UpdateUI();
                return true;
            }
            catch (MoneyException)
            {

                ReloadGame();
            }
        }
        return false;
    }

    public void CalculateTownIncome()
    {
        try
        {
            likesScript.likesTaxed(uiController.moneyMultiplier);
            moneyHelper.moneyMultiplier = uiController.moneyMultiplier;
            moneyHelper.CalculateMoney(buildingManager.GetAllStructures());
            UpdateUI();
        }
        catch (MoneyException)
        {
            ReloadGame();
        }
    }

    private void OnDisable()
    {
        CancelInvoke();   
    }

    public void UpdateUI()
    {
        uiController.UpdateLevel();
        uiController.SetMoneyValue(moneyHelper.Money);
        uiController.setPopulationValue(populationScript.Population);
        uiController.setLikesValue(likesScript.taxedLikes);
    }

    public void IncreaseMoney(int amount)
    {
        moneyHelper.IncreaseMoney(amount);
        UpdateUI();
    }

    private void ReloadGame()
    {
        Debug.Log("End Game");
    }

    public bool Purchasable(int amount)
    {
        if (moneyHelper.Money >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int structurePurchasingPower(int placementCost, int structureCount)
    {
        int amount = (int)(moneyHelper.Money / placementCost);
        return amount > structureCount ? structureCount : amount;
    }
    public void IncreasePopulation(int value)
    {
        populationScript.IncreasePopulation(value);
        UpdateUI();
    }
    public void DecreasePopulation(int value)
    {
        populationScript.DecreasePopulation(value);
        UpdateUI();
    }
    public void IncreaseLikes(int value)
    {
        likesScript.IncreaseLikes(value);
        UpdateUI();
    }
    public void DecreaseLikes(int value)
    {
        likesScript.DecreaseLikes(value);
        UpdateUI();
    }
    public void SetMoney(int amount)
    {
        moneyHelper.SetMoney(amount);
        UpdateUI();
    }
    public void SetPopulation(int value)
    {
        populationScript.SetPopulation(value);
        UpdateUI();
    }
    public void SetLikes(int value)
    {
        likesScript.SetLikes(value);
        UpdateUI();
    }

}
