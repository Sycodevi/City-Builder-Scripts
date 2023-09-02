using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikesScript
{
    PopulationScript population;
    public int likes = 0;
    public int taxedLikes = 1;

    //public LikesScript(int amount)
    //{
    //    this.likes = amount;
    //}

    public int Likes { get => likes; set => likes = value; }
    public void IncreaseLikes(int value)
    {
        Likes += value;
    }
    public void DecreaseLikes(int value)
    {
        Likes -= value;
    }
    public void SetLikes(int value)
    {
        Likes = value;
    }
    public void likesTaxed(int amount)
    {
        int thisAmount = amount;

        if(thisAmount == 0)
        {
            thisAmount = 1;
        }
        if (thisAmount > 60)
        {
            taxedLikes = ((Likes - thisAmount) * 3);
        }
        else
        {
            taxedLikes = ((Likes - thisAmount) * 2);
        }//Likes = (likes/100) * (likesMultiplier/amount);
    }
}
