using UnityEngine;

public class Pistol : SemiAuto, IHandleChangeWeapon
{
    public Pistol(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim, int weaponSlot) : base(attackMode, damage, rof, range, weaponType, currentWeaponShootAnim, weaponSlot)
    {
    }    

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsWeaponAnimPlaying())
        {
            GetPlayerWeaponManager().ChangeWeapon(GetPlayerWeaponManager().ExistingWeaponsData.PistolWeapon);
        }
    } 
}