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
            if (WeaponManager._timeToNextShot > 0)
                return;

            PerformAttack();
        }
    }

    public override void PerformAttack()
    {
        AnimService.Play(WeaponManager.CurrentAnim);
        WeaponManager._timeToNextShot = Rof;

        WeaponManager.PlaySound();
        CreateRay(Range);

        //Debug.Log("Current weapon shooting mode: " + _shootingMode);
    }
}