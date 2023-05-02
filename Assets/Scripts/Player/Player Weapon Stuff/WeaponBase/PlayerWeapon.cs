using UnityEditor.Animations;
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

    public AnimatorController _animatorController;
    public Sprite _sprite;

    public PlayerWeaponManager WeaponManager { get; private set; }

    public PlayerWeapon(WeaponSO weaponData)
    {
        _shootingMode = weaponData.AttackMode;
        _damage = weaponData.Damage;
        _rof = weaponData.Rof;
        _range = weaponData.Range;
        _weaponType = weaponData.WeaponType;
        _currentWeaponShootAnim = weaponData.CurrentWeaponShootAnim;
        _weaponSlot = weaponData.WeaponSlot;

        _animatorController = weaponData.WeaponAnimatorController;
        _sprite = weaponData.WeaponSprite;
    }

    public abstract void PerformAttack();
    public abstract void HandleAttackInput();

    public PlayerWeaponManager GetPlayerWeaponManager()
    {
        if (WeaponManager != null)
            return WeaponManager;


        if (GameManager.PlayerObj.TryGetComponent<PlayerWeaponManager>(out PlayerWeaponManager weaponManager))
        {
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
        if (GetPlayerWeaponManager().WeaponHandlerAnimator.GetCurrentAnimatorStateInfo(0).IsName(GetPlayerWeaponManager().CurrentAnim) && GetPlayerWeaponManager().WeaponHandlerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return true;
        else
            return false;
    }

    //Plays standard shooting anim (for semi/melee or main part of full auto shooting anim)
    public void PlayAttackAnim()
    {
        GetPlayerWeaponManager().WeaponHandlerAnimator.Play(GetPlayerWeaponManager().CurrentAnim);
    }

    //For canceling full auto shooting anim when ammo equals 0
    public void CanFullAutoShootAnim(bool canShoot)
    {
        GetPlayerWeaponManager().WeaponHandlerAnimator.SetBool("canShoot", canShoot);
    }

    //Plays animation of elevating / lowering full-auto gun
    public void PlayAfterFullAutoShootAnim(PlayerWeapon currentWeapon)
    {
        GetPlayerWeaponManager().WeaponHandlerAnimator.Play(currentWeapon._weaponType.ToString() + "_after_shoot");
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