using UnityEngine;

public class Knife : Melee, IHandleChangeWeapon
{
    public Knife(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
    {
    }

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsWeaponAnimPlaying())
        {
            GetPlayerWeaponManager().ChangeWeapon(GetPlayerWeaponManager().ExistingWeaponsData.KnifeWeapon);
        }
    }
}