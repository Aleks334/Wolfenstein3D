using UnityEngine;

public abstract class Melee : PlayerWeapon
{
    public Melee(WeaponSO weaponData) : base(weaponData)
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
        GetPlayerWeaponManager()._timeToNextShot = Rof;
        CreateRay(Range);

        //Debug.Log("tryb strzelania aktualnej broni: " + _shootingMode);
    }
}