using UnityEngine;

public class Knife : PlayerWeapon
{
    public Knife(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
    {
    }

    public override void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Debug.Log("cooldown: " + timeToNextShot);
            if (GetPlayerWeaponManager()._timeToNextShot > 0)
                return;

            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        PlayAttackAnim();
        GetPlayerWeaponManager()._timeToNextShot = _rof;
        CreateRay(_range);

      //  Debug.Log("Player used knife. Ammo is the same: " + GetPlayerWeaponManager().PlayerData.playerAmmo.CurrentAmmo);
        Debug.Log("tryb strzelania aktualnej broni: " + _shootingMode);
    }
}
