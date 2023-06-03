using UnityEngine;

public class FullAuto : PlayerWeapon
{
    public FullAuto(WeaponSO weaponData) : base(weaponData)
    {
    }

    public override void HandleAttackInput()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (WeaponManager._timeToNextShot > 0)
                return;

            PerformAttack();
        }
        // Animation after full auto shot.
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            AnimService.PlayAfterFullAutoShootAnim(this);
    }

    public override void PerformAttack()
    {
        if (WeaponManager.AmmoManager.Data.CurrentAmmo == 0)
        {
            AnimService.CanFullAutoShootAnim(false);
            return;
        }

        AnimService.CanFullAutoShootAnim(true);
        WeaponManager.AmmoManager.RemoveAmmo();
        AnimService.Play(WeaponManager.CurrentAnim);
        WeaponManager._timeToNextShot = Rof;

        WeaponManager.PlaySound();
        CreateRay(Range);
        //Debug.Log("Shooting mode of current weapon: " + ShootingMode);
    }
}
