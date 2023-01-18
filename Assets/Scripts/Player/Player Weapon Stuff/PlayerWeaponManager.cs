using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    public string currentAnim;
    public WeaponType _currentWeapon;

    GameObject weaponsHandler;
    public GameObject[] weaponObjects = new GameObject[4];

    //For animations
   [SerializeField] Animator currentWeaponAnimator;
    
    void Awake()
    {
        weaponsHandler = GameObject.FindGameObjectWithTag("WeaponsHandler");

        for(int i = 0; i < weaponObjects.Length; i++)
        {
            weaponObjects[i] = weaponsHandler.transform.GetChild(i).gameObject;
        } 
    }
    void Start()
    {
        for (int i = 0; i < playerData._weaponsInInventory.Length; i++)
        {
            playerData._weaponsInInventory[i] = WeaponType.None;
        }
        //adding default weapons for player and setting current weapon
        DefaultWeapons(true);
    }
  
    void Update()
    {
        // TESTS WEAPON CHANGING SYSTEM - tutaj bêd¹ dodawane ify z kolejnymi KeyCode.Alpha dla innych broni
        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsWeaponAnimPlaying())
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

        }

    }

    public void ChangeWeapon(WeaponType weaponToChange)
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
       
    }
    //Plays standard shooting anim (for semi/melee or main part of full auto shooting anim)
    public void PlayShootAnim(string animToPlay)
    {
       currentWeaponAnimator.Play(animToPlay);
    }

    //For canceling full auto shooting anim when ammo equals 0
    public void CanFullAutoShootAnim(bool canShoot)
    {
        currentWeaponAnimator.SetBool("canShoot", canShoot);
    }

    //Plays animation of elevating / lowering full-auto gun
    public void PlayAfterFullAutoShootAnim()
    {
            currentWeaponAnimator.Play(PlayerWeapon.playerWeapons[_currentWeapon]._weaponType.ToString() + "_after_shoot");
    }

    bool IsWeaponAnimPlaying()
    {
        if (currentWeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentAnim) && currentWeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return true;
        else
            return false;
    }

    public void DefaultWeapons(bool reset)
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
    }

    public void GiveWeapon(WeaponType newWeapon)
    {
        Debug.Log("playerData._weaponsInInventory[" + (int)newWeapon + "] = " + newWeapon);
        playerData._weaponsInInventory[(int)newWeapon] = newWeapon;
    }

    public bool HaveThatWeapon(WeaponType weaponType)
    {
        foreach (WeaponType weapon in playerData._weaponsInInventory)
        {
            if (weaponType == weapon && weaponType != WeaponType.None)
                return true;
        }
        return false;
    }
}
