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
            if (WeaponManager._timeToNextShot > 0)
                return;

            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        if (WeaponManager.AmmoManager.Data.CurrentAmmo == 0)
            return;

        WeaponManager.AmmoManager.RemoveAmmo();
        AnimService.Play(WeaponManager.CurrentAnim);
        WeaponManager._timeToNextShot = Rof;

        WeaponManager.PlaySound();
        CreateRay(Range);
        //Debug.Log("tryb strzelania aktualnej broni: " + ShootingMode);
    }
}
