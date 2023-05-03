using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public WeaponsInventorySO _playerWeapons;
    public AmmoManager AmmoManager { get; private set; }

    private GameObject _weaponHandler;
   // public GameObject[] weaponObjects = new GameObject[4];

    //For animations
    public string CurrentAnim { get; private set; }
    public Animator WeaponHandlerAnimator { get; private set; }

    public float _timeToNextShot = 0f;
    public Camera PlayerCam { get; private set; }

    public PlayerWeapon CurrentWeapon { get; private set; }

    [SerializeField] private AllWeaponsData _existingWeaponsData;
    public AllWeaponsData ExistingWeaponsData
    {
        get { return _existingWeaponsData; }

        private set { _existingWeaponsData = value; }
    }

    void Awake()
    {
        PlayerCam = Camera.main;
        AmmoManager = GetComponent<AmmoManager>();

        ExistingWeaponsData.StartUp();

        _weaponHandler = GameObject.FindGameObjectWithTag("WeaponsHandler");
        WeaponHandlerAnimator = _weaponHandler.GetComponent<Animator>();
        /*
        for(int i = 0; i < weaponObjects.Length; i++)
        {
            weaponObjects[i] = weaponsHandler.transform.GetChild(i).gameObject;
        }*/
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
            Debug.Log("Slot " + i + ": " + PlayerData.WeaponsInInventory[i]);
         }
         */
    }

    public void GiveWeapon(PlayerWeapon newWeapon)
    {
        //  Debug.Log("playerData.WeaponsInInventory = " + newWeapon);
        _playerWeapons.WeaponsInInventory[newWeapon.WeaponSlot] = newWeapon;

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
            Debug.Log("Gracz ju¿ trzyma " + weaponToChange);
            return;
        }

        CurrentWeapon = _playerWeapons.WeaponsInInventory[weaponToChange.WeaponSlot];
        Debug.Log("Aktywna broñ: " + CurrentWeapon);

        
        _weaponHandler.GetComponent<SpriteRenderer>().sprite = CurrentWeapon.WeaponSprite;
        _weaponHandler.GetComponent<Animator>().runtimeAnimatorController = CurrentWeapon.AnimatorController;

        CurrentAnim = CurrentWeapon.WeaponAttackAnim;
        UI.ReloadUI();
    }
}