using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour, IAudio
{
    private GameObject _weaponHandler;

    public WeaponsInventorySO _playerWeapons;
    public AmmoManager AmmoManager { get; private set; }
    public string CurrentAnim { get; private set; }
    public Animator WeaponHandlerAnimator { get; private set; }

    public float _timeToNextShot = 0f;
    public Camera PlayerCam { get; private set; }

    public PlayerWeapon CurrentWeapon { get; private set; }

    [SerializeField] private AllWeaponsData _existingWeaponsData;
    public AllWeaponsData ExistingWeaponsData
    {
        get => _existingWeaponsData;
    }

    void Awake()
    {
        PlayerCam = transform.GetChild(0).GetComponent<Camera>();
        AmmoManager = GetComponent<AmmoManager>();

        ExistingWeaponsData.StartUp();

        _weaponHandler = GameObject.FindGameObjectWithTag("WeaponsHandler");
        WeaponHandlerAnimator = _weaponHandler.GetComponent<Animator>();
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
    }

    public void GiveWeapon(PlayerWeapon newWeapon)
    {
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
           // Debug.Log("Player already handle " + weaponToChange);
            return;
        }

        CurrentWeapon = _playerWeapons.WeaponsInInventory[weaponToChange.WeaponSlot];
        _playerWeapons.CurrentPlayerWeapon = this.CurrentWeapon;

        //Debug.Log("Active weapon: " + CurrentWeapon);
        
        _weaponHandler.GetComponent<SpriteRenderer>().sprite = CurrentWeapon.WeaponSprite;
        _weaponHandler.GetComponent<Animator>().runtimeAnimatorController = CurrentWeapon.AnimatorController;
        CurrentAnim = CurrentWeapon.WeaponAttackAnim;

        ChangeAudioCueCurrentWeapon(CurrentWeapon);
        UI.ReloadUI();
    }

    public void PlaySound()
    {
        if (_weaponHandler.TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.PlayAudioCue();
        else
            Debug.LogError("_weaponHandler doesn't have AudioCue");
    }

    private void ChangeAudioCueCurrentWeapon(PlayerWeapon currentWeapon)
    {
        if (_weaponHandler.TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.AudioData = currentWeapon.WeaponAttackSound;
        else
            Debug.LogError("_weaponHandler doesn't have AudioCue");
    }
}