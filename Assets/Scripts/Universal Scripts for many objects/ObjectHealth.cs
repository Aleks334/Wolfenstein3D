using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectHealth
{
    // Skrypt do obs³ugi zdrowia dowolnego obiektu w grze.

    // Fields
    int _currentHealth;
    int _maxHealth;

    //Properites
    public int CurrentHealth
    {
        get { return _currentHealth; }

        set { _currentHealth = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }

        private set { _maxHealth = value; }
    }

    //Constructor
    public ObjectHealth(int health, int maxHealth)
    {
        CurrentHealth = health;
        MaxHealth = maxHealth;
    }

    //Methods
    public void DmgValue(int dmgAmount)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth -= dmgAmount;
        }
        if (CurrentHealth < 0)
            CurrentHealth = 0;
    }

    public void HealingValue(int healingAmount)
    {
        if (CurrentHealth < MaxHealth)
            CurrentHealth += healingAmount;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;  
    }
}
