using UnityEngine;

public abstract class Melee : PlayerWeapon
{
    public Melee(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
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
        PlayAttackAnim();
        GetPlayerWeaponManager()._timeToNextShot = _rof;
        CreateRay(_range);

        Debug.Log("tryb strzelania aktualnej broni: " + _shootingMode);
    }
}