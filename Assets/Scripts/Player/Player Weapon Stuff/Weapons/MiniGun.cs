using UnityEngine;

public class MiniGun : FullAuto, IHandleChangeWeapon
{
    public MiniGun(WeaponSO weaponData) : base(weaponData)
    {
    }

    public void HandleChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !AnimService.IsPlaying(WeaponManager.CurrentAnim))
        {
            WeaponManager.ChangeWeapon(WeaponManager.ExistingWeaponsData.MiniGun);
        }
    }
}