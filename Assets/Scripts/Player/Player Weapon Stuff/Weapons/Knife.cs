using UnityEngine;

public class Knife : Melee, IHandleChangeWeapon
{
    public Knife(WeaponSO weaponData) : base(weaponData)
    {
        
    }

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsWeaponAnimPlaying())
        {
            GetPlayerWeaponManager().ChangeWeapon(GetPlayerWeaponManager().ExistingWeaponsData.Knife);
        }
    }
}