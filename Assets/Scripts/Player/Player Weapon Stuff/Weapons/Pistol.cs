using UnityEngine;

public class Pistol : SemiAuto, IHandleChangeWeapon
{
    public Pistol(WeaponSO weaponData) : base(weaponData)
    {
    }    

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !AnimService.IsPlaying(WeaponManager.CurrentAnim))
        {
            WeaponManager.ChangeWeapon(WeaponManager.ExistingWeaponsData.Pistol);
        }
    } 
}