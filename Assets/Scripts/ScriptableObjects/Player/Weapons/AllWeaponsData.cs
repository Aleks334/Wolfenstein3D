using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons/All Weapons Data Container")]
public class AllWeaponsData : ScriptableObject
{
    public Knife Knife { get; private set; }
    public Pistol Pistol { get; private set; }
    public MachineGun MachineGun { get; private set; }
    public MiniGun MiniGun { get; private set; }

    private Dictionary<WeaponType, PlayerWeapon> Weapons = new Dictionary<WeaponType, PlayerWeapon>();

    public List<PlayerWeapon> AllWeaponsList = new List<PlayerWeapon>();

    //Visible in inspector
    [SerializeField] private List<WeaponSO> WeaponsData = new List<WeaponSO>();

    public void StartUp()
    {
        if (Weapons.Count != 0 && AllWeaponsList.Count != 0)
            CleanUp();

        Knife = new Knife(WeaponsData[0]);
        Weapons.Add(WeaponType.knife, Knife);
        AllWeaponsList.Add(Knife);

        Pistol = new Pistol(WeaponsData[1]);
        Weapons.Add(WeaponType.pistol, Pistol);
        AllWeaponsList.Add(Pistol);

        MachineGun = new MachineGun(WeaponsData[2]);
        Weapons.Add(WeaponType.machine_gun, MachineGun);
        AllWeaponsList.Add(MachineGun);

        MiniGun = new MiniGun(WeaponsData[3]);
        Weapons.Add(WeaponType.mini_gun, MiniGun);
        AllWeaponsList.Add(MiniGun);
    }

    private void CleanUp()
    {
        Weapons.Clear();
        AllWeaponsList.Clear();
    }

    //Type converter for weapon items
    public PlayerWeapon WeaponTypeToPlayerWeapon(WeaponType weapon)
    {
        return Weapons[weapon];
    }
}