using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHelper : MonoBehaviour
{
    public int money;
    public int moneyMultiplier = 0;

    public MoneyHelper(int startMoneyAmount)
    {
        this.money = startMoneyAmount;
    }
    

    public int Money 
    { 
        get => money; 
        private set 
        { 
            if(money < 0)
            {
                money = 0;
                throw new MoneyException("Not Enough Money");
            }
            else
            {
                money = value;
            }
        } 
    }

    public void DecreaseMoney(int amount)
    {
        Money -= amount;
    }

    public void IncreaseMoney(int amount)
    {
        Money += amount;
    }

    public void SetMoney(int amount)
    {
        Money = amount;
    }

    public void CalculateMoney(IEnumerable<StructureBase> buildings)
    {
        CollectIncome(buildings);
        ReduceUpkeep(buildings);
    }

    private void ReduceUpkeep(IEnumerable<StructureBase> buildings)
    {
        foreach(var structure in buildings)
        {
            Money -= structure.upkeepCost;
        }
    }

    private void CollectIncome(IEnumerable<StructureBase> buildings)
    {
        foreach (var structure in buildings)
        {
            if(structure.GetType() == typeof(ZoneStructureObj)
                && structure.PowerProvider == true && structure.WaterProvider == true
                && structure.RoadProvider == true)
            {
                Money += ((structure.GetIncome() / 100) * moneyMultiplier);
            }
            else if(structure.GetType() == typeof(SingleFacilityObj))
            {
                Money += structure.GetIncome();
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
