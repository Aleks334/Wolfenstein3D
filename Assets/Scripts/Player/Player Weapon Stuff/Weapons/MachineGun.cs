using UnityEngine;

public class MachineGun : FullAuto, IHandleChangeWeapon
{
    public MachineGun(WeaponSO weaponData) : base(weaponData)
    {
    }

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !AnimService.IsPlaying(GetPlayerWeaponManager().CurrentAnim))
        {
            GetPlayerWeaponManager().ChangeWeapon(GetPlayerWeaponManager().ExistingWeaponsData.MachineGun);
        }
    }
}