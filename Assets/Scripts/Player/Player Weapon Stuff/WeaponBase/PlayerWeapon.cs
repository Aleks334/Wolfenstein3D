using UnityEditor.Animations;
using UnityEngine;

public abstract class PlayerWeapon
{
    protected int Damage { get; private set; }
    protected float Range { get; private set; }
    protected float Rof { get; private set; }
    protected ShootingMode ShootingMode { get; private set; }
    protected WeaponType WeaponType { get; private set; }

    public int WeaponSlot { get; private set; }
    public string WeaponAttackAnim { get; private set; }
    public AnimatorController AnimatorController { get; private set; }
    public Sprite WeaponSprite { get; private set; }
    public AudioCueSO WeaponAttackSound { get; private set; }

    public PlayerWeaponManager WeaponManager { get; private set; }

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
                enemy.Dmgenemy(GetPlayerWeaponManager().CurrentWeapon.Damage);
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
        GetPlayerWeaponManager().WeaponHandlerAnimator.Play(currentWeapon.WeaponType.ToString() + "_after_shoot");
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