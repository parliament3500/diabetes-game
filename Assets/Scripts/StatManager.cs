using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour 
{
    public int maxToxin;
    public int maxHunger;
    public static float toxin;
    public static float hunger;
    public delegate void myDel();
    public static event myDel death;
    public float toxinReductionvalue;
    public float hungerReductionValue;

        public void ToxinHalf()
    {
        toxin = maxToxin/2;
    }
    /// <param name="float1">Value to reduce Toxins</param>
    public static void ToxinReduce(float value)
    {
        toxin -= value;
        if(toxin < 0)
        {
            toxin = 0;
            if(death != null)
            death();
        }
    }
    /// <param name="float1">Value to increase Toxins</param>
    public void ToxinIncrease(float value)
    {
        toxin += value;
        if(toxin > maxToxin)
        {
            toxin = maxToxin;
            if(death != null)
            death();
        }
    }
    public void HungerHalf()
    {
        hunger = maxHunger/2;
    }
    /// <param name="float1">Value to reduce Hunger</param>
    public static void HungerReduce(float value)
    {
        hunger -= value;
        if(hunger < 0)
        {
            hunger = 0;
            if(death != null)
            death();
        }
    }
    /// <param name="float1">Value to increase hunger</param>
    public void HungerIncrease(float value)
    {
        hunger += value;
        if(hunger > maxHunger)
        {
            hunger = maxHunger;
        }
    }

    public float ToxinReductionValue()
    {
        var temp = toxinReductionvalue * Time.deltaTime;
        return temp;
    }

    public float HungerReductionValue()
    {
        var temp = hungerReductionValue * Time.deltaTime;
        return temp;
    }
        
}

