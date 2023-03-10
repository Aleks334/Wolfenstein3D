using UnityEngine;

public abstract class PlayerWeapon
{
    public int _damage;
    public float _range;
    public float _rof;
    public ShootingMode _shootingMode;
    public WeaponType _weaponType;
    public string _currentWeaponShootAnim;
    public int _weaponSlot;

    public PlayerWeaponManager WeaponManager { get; private set; }

    public PlayerWeapon(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot)
    {
        _shootingMode = attackMode;
        _damage = damage;
        _rof = rof;
        _range = range;
        _weaponType = weaponType;
        _currentWeaponShootAnim = currentWeaponShootAnim;
        _weaponSlot = weaponSlot;
    }

    public abstract void PerformAttack();
    public abstract void HandleAttackInput();

    public PlayerWeaponManager GetPlayerWeaponManager()
    {
        if (WeaponManager != null)
            return WeaponManager;


        if (GameManager.PlayerObj.TryGetComponent<PlayerWeaponManager>(out PlayerWeaponManager weaponManager))
        {
            //Debug.LogWarning("Znaleziono PlayerWeaponManager (ammo)!");
            WeaponManager = weaponManager;
            return WeaponManager;
        }
        else
        {
            Debug.LogError("Gracz nie ma dodanej klasy PlayerWeaponManager!");
            return null;
        }
    }

    protected void CreateRay(float range)
    {
        RaycastHit hit;

        if (Physics.Raycast(GetPlayerWeaponManager().PlayerCam.transform.position, GetPlayerWeaponManager().PlayerCam.transform.forward, out hit, range))
        {
            if (hit.transform.TryGetComponent<enemystats>(out enemystats enemy))
            {
                enemy.Dmgenemy(GetPlayerWeaponManager().CurrentWeapon._damage);
            }
            Debug.DrawRay(GetPlayerWeaponManager().PlayerCam.transform.position, GetPlayerWeaponManager().PlayerCam.transform.forward * range, Color.green, 0.25f);
        }
        else
        {
            Debug.DrawRay(GetPlayerWeaponManager().PlayerCam.transform.position, GetPlayerWeaponManager().PlayerCam.transform.forward * range, Color.red, 0.25f);
        }
    }
    public bool IsWeaponAnimPlaying()
    {
        if (GetPlayerWeaponManager().CurrentWeaponAnimator.GetCurrentAnimatorStateInfo(0).IsName(GetPlayerWeaponManager().CurrentAnim) && GetPlayerWeaponManager().CurrentWeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return true;
        else
            return false;
    }

    //Plays standard shooting anim (for semi/melee or main part of full auto shooting anim)
    public void PlayAttackAnim()
    {
        GetPlayerWeaponManager().CurrentWeaponAnimator.Play(GetPlayerWeaponManager().CurrentAnim);
    }

    //For canceling full auto shooting anim when ammo equals 0
    public void CanFullAutoShootAnim(bool canShoot)
    {
        GetPlayerWeaponManager().CurrentWeaponAnimator.SetBool("canShoot", canShoot);
    }

    //Plays animation of elevating / lowering full-auto gun
    public void PlayAfterFullAutoShootAnim(PlayerWeapon currentWeapon)
    {
        GetPlayerWeaponManager().CurrentWeaponAnimator.Play(currentWeapon._weaponType.ToString() + "_after_shoot");
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