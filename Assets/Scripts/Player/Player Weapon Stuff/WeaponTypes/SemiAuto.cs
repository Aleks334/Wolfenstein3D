using UnityEngine;

public class SemiAuto : PlayerWeapon
{
    public SemiAuto(WeaponSO weaponData) : base(weaponData)
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
        GetPlayerWeaponManager()._timeToNextShot = Rof;

        CreateRay(Range);
        Debug.Log("tryb strzelania aktualnej broni: " + ShootingMode);
    }
}
