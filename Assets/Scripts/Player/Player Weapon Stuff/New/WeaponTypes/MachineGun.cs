using UnityEngine;

public class MachineGun : PlayerWeapon
{
    public MachineGun(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
    {
    }

    public override void HandleAttackInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (GetPlayerWeaponManager()._timeToNextShot > 0)
                return;

            PerformAttack();
        }
        // Animation after full auto shot.
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            PlayAfterFullAutoShootAnim(this);
    }

    public override void PerformAttack()
    {
        if (GetPlayerWeaponManager().PlayerData.playerAmmo.CurrentAmmo == 0)
        {
            CanFullAutoShootAnim(false);
            return;
        }

        CanFullAutoShootAnim(true);
        GetPlayerWeaponManager().PlayerStats.RemoveAmmo();
        PlayAttackAnim();
        GetPlayerWeaponManager()._timeToNextShot = _rof;

        CreateRay(_range);
        Debug.Log("tryb strzelania aktualnej broni: " + _shootingMode);
    }
}
