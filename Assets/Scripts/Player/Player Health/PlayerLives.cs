using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives
{
    //Fields
    private int _currentLives;

    //Properties
    public int CurrentLives
    { 
        get
        { 
            return _currentLives; 
        } 

        set
        {
            _currentLives = value;
        }
    }

    //Constructor
    public PlayerLives(int currentLives)
    {
        CurrentLives = currentLives;
    }

    //Methods
    public void IncreasePlayerLives(int livesToIncrease)
    {
        CurrentLives += livesToIncrease;
    }

    public void DecreasePlayerLives(int livesToDecrease)
    {
        if(CurrentLives > 0)
            CurrentLives -= livesToDecrease;
        if (CurrentLives < 0)
            CurrentLives = 0;
    }
}
