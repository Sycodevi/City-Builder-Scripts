using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationScript 
{
    public int population = 0;

    //public PopulationScript(int amount) 
    //{
    //    this.population = amount;
    //}
    public int Population { get => population; set => population = value; }
    public void IncreasePopulation(int value)
    {
        Population += value;
    }
    public void DecreasePopulation(int value)
    {
        Population -= value;
    }
    public void SetPopulation(int value)
    {
        Population = value;
    }

}
