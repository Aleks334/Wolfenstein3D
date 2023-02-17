using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo
{

    //Fields
    private int _currentAmmo;
    private int _maxAmmo;
    public const int StartAmmo = 8;

    //Properties
    public int CurrentAmmo
    {
        get { return _currentAmmo; }

        set { _currentAmmo = value; }
    }

    public int MaxAmmo
    {
        get { return _maxAmmo; }

        set { _maxAmmo = value; }
    }

    //Constructor
    public PlayerAmmo(int currentAmmo = StartAmmo, int maxAmmo = 99) // these values are used in original game
    {
        CurrentAmmo = currentAmmo;
        MaxAmmo = maxAmmo;
    }

    //Methods
    public void AddAmmo(int ammoToAdd)
    {
        if (CurrentAmmo < MaxAmmo)
        {
            CurrentAmmo += ammoToAdd;

            if (CurrentAmmo > MaxAmmo)
                CurrentAmmo = MaxAmmo;
        }
    }

    public void RemoveAmmo()
    {
        if (CurrentAmmo > 0)
            CurrentAmmo--;

        if (CurrentAmmo <= 0)
            CurrentAmmo = 0;
    }
}