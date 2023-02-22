using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public PlayerData PlayerData;

    public PlayerStats PlayerStats { get; private set; }

    public string currentAnim;
    public WeaponType _currentWeapon;

    GameObject weaponsHandler;
    public GameObject[] weaponObjects = new GameObject[4];

    //For animations
    public Animator CurrentWeaponAnimator { get; private set; }

    public float _timeToNextShot = 0f;
    public Camera PlayerCam { get; private set; }


    public PlayerWeapon CurrentWeapon { get; private set; }
    public PlayerWeaponsData ExistingWeaponsData { get; private set; }

    void Awake()
    {
        weaponsHandler = GameObject.FindGameObjectWithTag("WeaponsHandler");

        for(int i = 0; i < weaponObjects.Length; i++)
        {
            weaponObjects[i] = weaponsHandler.transform.GetChild(i).gameObject;
        }

        PlayerCam = Camera.main;

        PlayerStats = GetComponent<PlayerStats>();

        ExistingWeaponsData = new PlayerWeaponsData();
    }
    void Start()
    {
      /*  for (int i = 0; i < playerData._weaponsInInventory.Length; i++)
        {
            playerData._weaponsInInventory[i] = WeaponType.None;
        }*/

        //adding default weapons for player and setting current weapon
        _DefaultWeapons();
    }
  
    void Update()
    {
        _timeToNextShot -= Time.deltaTime;

        CurrentWeapon.HandleAttackInput();
        // TESTS WEAPON CHANGING SYSTEM - tutaj bêd¹ dodawane ify z kolejnymi KeyCode.Alpha dla innych broni
        /*  if (Input.GetKeyDown(KeyCode.Alpha1) && !IsWeaponAnimPlaying())
          {
              if (_currentWeapon != WeaponType.knife)
              {
                  ChangeWeapon(WeaponType.knife);
              }
              else
              {
                  Debug.Log("Gracz aktualnie trzyma tê broñ");
              }
          }
          else if (Input.GetKeyDown(KeyCode.Alpha2) && !IsWeaponAnimPlaying())
          {
              if (_currentWeapon != WeaponType.pistol)
              {
                  ChangeWeapon(WeaponType.pistol);
              }
              else
              {
                  Debug.Log("Gracz aktualnie trzyma tê broñ");
              }

          }
          else if (Input.GetKeyDown(KeyCode.Alpha3) && !IsWeaponAnimPlaying())
          {
              if (_currentWeapon != WeaponType.machine_gun)
              {
                  ChangeWeapon(WeaponType.machine_gun);
              }
              else
              {
                  Debug.Log("Gracz aktualnie trzyma tê broñ");
              }

          }
          else if (Input.GetKeyDown(KeyCode.Alpha4) && !IsWeaponAnimPlaying())
          {
              if (_currentWeapon != WeaponType.mini_gun)
              {
                  ChangeWeapon(WeaponType.mini_gun);
              }
              else
              {
                  Debug.Log("Gracz aktualnie trzyma tê broñ");
              }

          }*/

        //NEW:
        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsWeaponAnimPlaying())
        {
             _ChangeWeapon(ExistingWeaponsData.KnifeWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !IsWeaponAnimPlaying())
        {
            _ChangeWeapon(ExistingWeaponsData.PistolWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !IsWeaponAnimPlaying())
        {
            _ChangeWeapon(ExistingWeaponsData.MachineGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !IsWeaponAnimPlaying())
        {
           // _ChangeWeapon(WeaponType.mini_gun);
        }
    }

    public void _ChangeWeapon(PlayerWeapon weaponToChange)
    {
        if (_HaveThatWeapon(weaponToChange) && CurrentWeapon != weaponToChange)
        {
            CurrentWeapon = PlayerData.WeaponsInInventory[weaponToChange._weaponSlot];
            Debug.Log("Aktywna broñ: " + CurrentWeapon);

            //Find animator for current weapon
            CurrentWeaponAnimator = weaponObjects[(int)CurrentWeapon._weaponSlot].GetComponent<Animator>();

            //Enable current weapon gameobject and disable other.
            weaponObjects[(int)CurrentWeapon._weaponSlot].SetActive(true);
            for (int i = 0; i < weaponObjects.Length; i++)
            {
                if (i != (int)CurrentWeapon._weaponSlot)
                    weaponObjects[i].SetActive(false);
            }

            currentAnim = CurrentWeapon._currentWeaponShootAnim;
          //  Debug.Log("Obecny obiekt broni: " + weaponObjects[(int)CurrentWeapon._weaponSlot]);
            UI.ReloadUI();
        }
        else
        {
            Debug.Log("Gracz nie posiada " + weaponToChange + " w ekwipunku lub ju¿ j¹ trzyma");
        }
    }

   /* public void ChangeWeapon(WeaponType weaponToChange)
    {
        if(HaveThatWeapon(weaponToChange))
        {
            _currentWeapon = playerData._weaponsInInventory[(int)weaponToChange];
            Debug.Log("Aktywna broñ: " + _currentWeapon);
            //Find animator for current weapon
            currentWeaponAnimator = weaponObjects[(int)_currentWeapon].GetComponent<Animator>();
            //Enable current weapon gameobject and disable other.
            weaponObjects[(int)_currentWeapon].SetActive(true);
            for (int i = 0; i < weaponObjects.Length; i++)
            {
                if (i != (int)_currentWeapon)
                    weaponObjects[i].SetActive(false);
            }
            currentAnim = PlayerWeapon.playerWeapons[_currentWeapon]._currentWeaponShootAnim;
            Debug.Log("Obecny obiekt broni: " + weaponObjects[(int)_currentWeapon]);
            UI.ReloadUI();
        } else
        {
            Debug.Log("Gracz nie posiada " + weaponToChange + " w ekwipunku");
        }
       
    }*/
    
    

    bool IsWeaponAnimPlaying()
    {
        if (CurrentWeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentAnim) && CurrentWeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return true;
        else
            return false;
    }

    //NEW:
    public void _DefaultWeapons()
    {
        for (int i = 0; i < PlayerData.WeaponsInInventory.Length; i++)
        {
           PlayerData.WeaponsInInventory[i] = null;
        }

        _GiveWeapon(ExistingWeaponsData.PistolWeapon);
        _GiveWeapon(ExistingWeaponsData.KnifeWeapon);

        for (int i = 0; i < PlayerData.WeaponsInInventory.Length; i++)
        {
           Debug.LogWarning("Slot " + i + ": " + PlayerData.WeaponsInInventory[i]);
        }
        
    }

    /*  public void DefaultWeapons(bool reset)
      {
          if(reset)
          {
              for(int i = 0; i < playerData._weaponsInInventory.Length; i++)
              {
                  playerData._weaponsInInventory[i] = WeaponType.None;
              }
          }

          GiveWeapon(WeaponType.knife);
          GiveWeapon(WeaponType.pistol);
          ChangeWeapon(WeaponType.pistol);
      }*/
    //NEW:
    public void _GiveWeapon(PlayerWeapon newWeapon)
    {
      //  Debug.Log("playerData.WeaponsInInventory[" + newWeapon + "] = " + newWeapon);
        PlayerData.WeaponsInInventory[newWeapon._weaponSlot] = newWeapon;

        _ChangeWeapon(newWeapon);
    }

  /*  public void GiveWeapon(WeaponType newWeapon)
    {
        Debug.Log("playerData._weaponsInInventory[" + (int)newWeapon + "] = " + newWeapon);
        playerData._weaponsInInventory[(int)newWeapon] = newWeapon;

        ChangeWeapon(newWeapon);
    }*/

    //NEW:
    public bool _HaveThatWeapon(PlayerWeapon weaponToCheck)
    {
        foreach (PlayerWeapon weapon in PlayerData.WeaponsInInventory)
        {
            if (weaponToCheck == weapon)
                return true;         
        }

        return false;
    }

   /* public bool HaveThatWeapon(WeaponType weaponType)
    {
        foreach (WeaponType weapon in playerData._weaponsInInventory)
        {
            if (weaponType == weapon && weaponType != WeaponType.None)
                return true;
        }
        return false;
    }*/
}
