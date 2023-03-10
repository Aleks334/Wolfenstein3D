using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public PlayerWeaponsSO _playerWeapons;
    public AmmoManager AmmoManager { get; private set; }

    GameObject weaponsHandler;
    public GameObject[] weaponObjects = new GameObject[4];

    //For animations
    public string CurrentAnim { get; private set; }
    public Animator CurrentWeaponAnimator { get; private set; }

    public float _timeToNextShot = 0f;
    public Camera PlayerCam { get; private set; }

    public PlayerWeapon CurrentWeapon { get; private set; }

    [SerializeField] private PlayerWeaponsData _existingWeaponsData;
    public PlayerWeaponsData ExistingWeaponsData
    {
        get { return _existingWeaponsData; }

        private set { _existingWeaponsData = value; }
    }

    void Awake()
    {
        weaponsHandler = GameObject.FindGameObjectWithTag("WeaponsHandler");

        for(int i = 0; i < weaponObjects.Length; i++)
        {
            weaponObjects[i] = weaponsHandler.transform.GetChild(i).gameObject;
        }

        PlayerCam = Camera.main;
        AmmoManager = GetComponent<AmmoManager>();

       // ExistingWeaponsData = new PlayerWeaponsData();
    }
    void Start()
    {
        DefaultWeapons();
    }
  
    void Update()
    {
        _timeToNextShot -= Time.deltaTime;

        CurrentWeapon.HandleAttackInput();


        foreach(IHandleChangeWeapon weapon in _playerWeapons.WeaponsInInventory)
        {  
            if (weapon != null)
                weapon.HandleChangeWeaponInput();
        }
    }

    public void DefaultWeapons()
    {
        for (int i = 0; i < _playerWeapons.WeaponsInInventory.Length; i++)
        {
            _playerWeapons.WeaponsInInventory[i] = null;
        }

        GiveWeapon(ExistingWeaponsData.Pistol);
        GiveWeapon(ExistingWeaponsData.Knife);

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
        _playerWeapons.WeaponsInInventory[newWeapon._weaponSlot] = newWeapon;

        ChangeWeapon(newWeapon);
    }

    public bool HaveThatWeapon(PlayerWeapon weaponToCheck)
    {
        foreach (PlayerWeapon weapon in _playerWeapons.WeaponsInInventory)
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
            Debug.Log("Gracz ju? trzyma " + weaponToChange);
            return;
        }

        CurrentWeapon = _playerWeapons.WeaponsInInventory[weaponToChange._weaponSlot];
        Debug.Log("Aktywna bro?: " + CurrentWeapon);

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