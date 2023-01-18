using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
{
    public static Dictionary<WeaponType, PlayerWeapon> playerWeapons = new Dictionary<WeaponType, PlayerWeapon>()
    {
        { WeaponType.knife, new PlayerWeapon(ShootingMode.meele, 9, 0.2f, 3f, WeaponType.knife, "knife_stabbing") },
        { WeaponType.pistol, new PlayerWeapon(ShootingMode.semi_auto, 18, 0.35f, 50f, WeaponType.pistol, "pistol_shooting") },
        { WeaponType.machine_gun, new PlayerWeapon(ShootingMode.full_auto, 25, 0.25f, 65f, WeaponType.machine_gun, "machine_gun_shooting") },
        { WeaponType.mini_gun, new PlayerWeapon(ShootingMode.full_auto, 30, 0.17f, 55f, WeaponType.mini_gun, "mini_gun_shooting") },
        // Add new weapons here.
    };

    public int _damage;
    public float _range;
    public float _rof;
    public ShootingMode _shootingMode;
    public WeaponType _weaponType;
    public string _currentWeaponShootAnim;

    public PlayerWeapon(ShootingMode attackMode, int damage, float rof, float range, WeaponType weaponType, string currentWeaponShootAnim)
    {
        _shootingMode = attackMode;
        _damage = damage;
        _rof = rof;
        _range = range;
        _weaponType = weaponType;
        _currentWeaponShootAnim = currentWeaponShootAnim;
    }
}

public enum ShootingMode
{
    meele,
    semi_auto,
    full_auto,
}

public enum WeaponType
{
    None  = -1,
    knife,
    pistol,
    machine_gun,
    mini_gun,
}