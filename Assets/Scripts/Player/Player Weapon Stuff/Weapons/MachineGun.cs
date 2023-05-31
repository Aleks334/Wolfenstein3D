using UnityEngine;

public class MachineGun : FullAuto, IHandleChangeWeapon
{
    public MachineGun(WeaponSO weaponData) : base(weaponData)
    {
    }

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !AnimService.IsPlaying(WeaponManager.CurrentAnim))
        {
            WeaponManager.ChangeWeapon(WeaponManager.ExistingWeaponsData.MachineGun);
        }
    }
}