using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsData
{
    public Knife KnifeWeapon { get; private set; }
    public Pistol PistolWeapon { get; private set; }
    public MachineGun MachineGun { get; private set; }
    public MiniGun MiniGun { get; private set; }

    public List<PlayerWeapon> ExistingWeapons = new List<PlayerWeapon>();

    public PlayerWeaponsData()
    {
        KnifeWeapon = new Knife(ShootingMode.meele, 9, 0.2f, 3f, WeaponType.knife, "knife_stabbing", 0);
        ExistingWeapons.Add(KnifeWeapon);

        PistolWeapon = new Pistol(ShootingMode.semi_auto, 18, 0.35f, 50f, WeaponType.pistol, "pistol_shooting", 1);
        ExistingWeapons.Add(PistolWeapon);

        MachineGun = new MachineGun(ShootingMode.full_auto, 25, 0.25f, 65f, WeaponType.machine_gun, "machine_gun_shooting", 2);
        ExistingWeapons.Add(MachineGun);

        MiniGun = new MiniGun(ShootingMode.full_auto, 30, 0.17f, 55f, WeaponType.mini_gun, "mini_gun_shooting", 3);
        ExistingWeapons.Add(MiniGun);
    }

    public PlayerWeapon ReturnWeaponClass(WeaponType weapon)
    {
        return ExistingWeapons[(int)weapon];
    }
}
