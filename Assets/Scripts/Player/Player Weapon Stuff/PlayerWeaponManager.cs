using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public PlayerData PlayerData;
    public PlayerStats PlayerStats { get; private set; }

    GameObject weaponsHandler;
    public GameObject[] weaponObjects = new GameObject[4];

    //For animations
    public string CurrentAnim { get; private set; }
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
        DefaultWeapons();
    }
  
    void Update()
    {
        _timeToNextShot -= Time.deltaTime;

        CurrentWeapon.HandleAttackInput();


        foreach(IHandleChangeWeapon weapon in ExistingWeaponsData.ExistingWeapons)
        {
            weapon.HandleChangeWeaponInput();
        }
    }

    public void DefaultWeapons()
    {
        for (int i = 0; i < PlayerData.WeaponsInInventory.Length; i++)
        {
           PlayerData.WeaponsInInventory[i] = null;
        }

        GiveWeapon(ExistingWeaponsData.PistolWeapon);
        GiveWeapon(ExistingWeaponsData.KnifeWeapon);

       /*
        for (int i = 0; i < PlayerData.WeaponsInInventory.Length; i++)
        {
           Debug.LogWarning("Slot " + i + ": " + PlayerData.WeaponsInInventory[i]);
        }
        */
    }

    public void GiveWeapon(PlayerWeapon newWeapon)
    {
      //  Debug.Log("playerData.WeaponsInInventory = " + newWeapon);
        PlayerData.WeaponsInInventory[newWeapon._weaponSlot] = newWeapon;

        ChangeWeapon(newWeapon);
    }

    public bool HaveThatWeapon(PlayerWeapon weaponToCheck)
    {
        foreach (PlayerWeapon weapon in PlayerData.WeaponsInInventory)
        {
            if (weaponToCheck == weapon)
                return true;         
        }

        return false;
    }

    public void ChangeWeapon(PlayerWeapon weaponToChange)
    {

        if (!HaveThatWeapon(weaponToChange) || CurrentWeapon == weaponToChange)
        {
            Debug.Log("Gracz nie posiada " + weaponToChange + " w ekwipunku lub ju¿ j¹ trzyma");
            return;
        }

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

        CurrentAnim = CurrentWeapon._currentWeaponShootAnim;
        //  Debug.Log("Obecny obiekt broni: " + weaponObjects[(int)CurrentWeapon._weaponSlot]);
        UI.ReloadUI();
    }
}