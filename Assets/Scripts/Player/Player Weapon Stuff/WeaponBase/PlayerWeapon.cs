using UnityEngine;

public abstract class PlayerWeapon
{
    private PlayerWeaponManager _weaponManager;
    protected PlayerWeaponManager WeaponManager
    {
        get
        {
            if (_weaponManager != null)
                return _weaponManager;

            if (GameManager.PlayerObj.TryGetComponent<PlayerWeaponManager>(out PlayerWeaponManager weaponManager))
            {
                _weaponManager = weaponManager;
                return _weaponManager;
            }
            else
            {
                Debug.LogError("Gracz nie ma dodanej klasy PlayerWeaponManager!");
                return null;
            }
        }

        private set => _weaponManager = value;
    }

    private WeaponAnimationService _animService;
    protected WeaponAnimationService AnimService
    {
        get => _animService ??= new WeaponAnimationService(WeaponManager.WeaponHandlerAnimator);
        private set => _animService = value;
    }

    protected int Damage { get; private set; }
    protected float Range { get; private set; }
    protected float Rof { get; private set; }
    protected ShootingMode ShootingMode { get; private set; } 

    public WeaponType WeaponType { get; private set; }
    public int WeaponSlot { get; private set; }
    public string WeaponAttackAnim { get; private set; }
    public RuntimeAnimatorController AnimatorController { get; private set; }
    public Sprite WeaponSprite { get; private set; }
    public AudioCueSO WeaponAttackSound { get; private set; }

    public PlayerWeapon(WeaponSO weaponData)
    {
        ShootingMode = weaponData.AttackMode;
        Damage = weaponData.Damage;
        Rof = weaponData.Rof;
        Range = weaponData.Range;
        WeaponType = weaponData.WeaponType;
        WeaponAttackAnim = weaponData.WeaponAttackAnim;
        WeaponSlot = weaponData.WeaponSlot;

        AnimatorController = weaponData.WeaponAnimatorController;
        WeaponSprite = weaponData.WeaponSprite;
        WeaponAttackSound = weaponData.WeaponAttackSound;
    }

    public abstract void PerformAttack();
    public abstract void HandleAttackInput();

    protected void CreateRay(float range)
    {
        RaycastHit hit;

        if (Physics.Raycast(WeaponManager.PlayerCam.transform.position,
                            WeaponManager.PlayerCam.transform.forward,
                            out hit, range))
        {
            //TODO: Replace with IDamageable interface.
            if (hit.transform.TryGetComponent<enemystats>(out enemystats enemy))
            {
                enemy.Dmgenemy(WeaponManager.CurrentWeapon.Damage);
            }

            Debug.DrawRay(WeaponManager.PlayerCam.transform.position,
                          WeaponManager.PlayerCam.transform.forward * range,
                          Color.green, 0.25f);
        }
        else
        {
            Debug.DrawRay(WeaponManager.PlayerCam.transform.position,
                          WeaponManager.PlayerCam.transform.forward * range,
                          Color.red, 0.25f);
        }
    }
}

public enum ShootingMode
{
    meele,
    semi_auto,
    full_auto,
}

public enum WeaponType
{
    knife,
    pistol,
    machine_gun,
    mini_gun,
}