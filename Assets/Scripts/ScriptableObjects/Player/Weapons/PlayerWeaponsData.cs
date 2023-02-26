using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapon/Data_Container")]
public class PlayerWeaponsData : ScriptableObject
{
    public Knife Knife { get; private set; }
    public Pistol Pistol { get; private set; }
    public MachineGun MachineGun { get; private set; }
    public MiniGun MiniGun { get; private set; }

    private Dictionary<WeaponType, PlayerWeapon> Weapons = new Dictionary<WeaponType, PlayerWeapon>();
    [SerializeField] private List<WeaponSO> WeaponSOs = new List<WeaponSO>();

    public List<PlayerWeapon> ExistingWeapons = new List<PlayerWeapon>();

    private void OnEnable()
    {
        Knife = new Knife(ShootingMode.meele, 9, 0.2f, 3f, WeaponType.knife, "knife_stabbing", 0);
        Weapons.Add(WeaponType.knife, Knife);
        ExistingWeapons.Add(Knife);

        Pistol = new Pistol(ShootingMode.semi_auto, 18, 0.35f, 50f, WeaponType.pistol, "pistol_shooting", 1);
        Weapons.Add(WeaponType.pistol, Pistol);
        ExistingWeapons.Add(Pistol);

        MachineGun = new MachineGun(ShootingMode.full_auto, 25, 0.25f, 65f, WeaponType.machine_gun, "machine_gun_shooting", 2);
        Weapons.Add(WeaponType.machine_gun, MachineGun);
        ExistingWeapons.Add(MachineGun);

        MiniGun = new MiniGun(ShootingMode.full_auto, 30, 0.17f, 55f, WeaponType.mini_gun, "mini_gun_shooting", 3);
        Weapons.Add(WeaponType.mini_gun, MiniGun);
        ExistingWeapons.Add(MiniGun);
        /*
        foreach (var weapon in WeaponSOs)
        {
            weapon._weapon = Weapons[weapon._weaponType];
        }
        */
    }

    public PlayerWeapon WeaponTypeToPlayerWeapon(WeaponType weapon)
    {
        return Weapons[weapon];
    }
}
