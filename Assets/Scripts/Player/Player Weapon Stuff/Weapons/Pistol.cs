using UnityEngine;

public class Pistol : SemiAuto, IHandleChangeWeapon
{
    public Pistol(WeaponSO weaponData) : base(weaponData)
    {
    }    

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !IsWeaponAnimPlaying())
        {
            GetPlayerWeaponManager().ChangeWeapon(GetPlayerWeaponManager().ExistingWeaponsData.Pistol);
        }
    } 
}