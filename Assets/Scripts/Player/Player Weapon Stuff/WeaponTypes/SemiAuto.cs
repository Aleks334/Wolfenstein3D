using UnityEngine;

public class SemiAuto : PlayerWeapon
{
    public SemiAuto(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
    {
    }

    public override void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (GetPlayerWeaponManager()._timeToNextShot > 0)
                return;

            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        if (GetPlayerWeaponManager().AmmoManager.Data.CurrentAmmo == 0)
            return;

        GetPlayerWeaponManager().AmmoManager.RemoveAmmo();
        PlayAttackAnim();
        GetPlayerWeaponManager()._timeToNextShot = _rof;

        CreateRay(_range);
        Debug.Log("tryb strzelania aktualnej broni: " + _shootingMode);
    }
}
