using UnityEngine;

public class Pistol : PlayerWeapon
{
    public Pistol(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
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

    public override void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsWeaponAnimPlaying())
        {
            GetPlayerWeaponManager()._ChangeWeapon(GetPlayerWeaponManager().ExistingWeaponsData.PistolWeapon);
        }
    }

    public override void PerformAttack()
    {
        if (GetPlayerWeaponManager().PlayerData.playerAmmo.CurrentAmmo == 0)
            return;

        GetPlayerWeaponManager().PlayerStats.RemoveAmmo();
        PlayAttackAnim();
        GetPlayerWeaponManager()._timeToNextShot = _rof;

        CreateRay(_range);
        Debug.Log("tryb strzelania aktualnej broni: " + _shootingMode);
    }
}
