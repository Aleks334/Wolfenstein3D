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
            if (GetPlayerWeaponManager()._timeToNextShot > 0)
                return;

            PerformAttack();
        }
        // Animation after full auto shot.
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            AnimService.PlayAfterFullAutoShootAnim(this);
    }

    public override void PerformAttack()
    {
        if (GetPlayerWeaponManager().AmmoManager.Data.CurrentAmmo == 0)
        {
            AnimService.CanFullAutoShootAnim(false);
            return;
        }

        AnimService.CanFullAutoShootAnim(true);
        GetPlayerWeaponManager().AmmoManager.RemoveAmmo();
        AnimService.Play(GetPlayerWeaponManager().CurrentAnim);
        GetPlayerWeaponManager()._timeToNextShot = Rof;

        GetPlayerWeaponManager().PlaySound();
        CreateRay(Range);
        Debug.Log("tryb strzelania aktualnej broni: " + ShootingMode);
    }
}
