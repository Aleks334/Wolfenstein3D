using UnityEngine;

public class WeaponSO : ScriptableObject
{
    public int _damage;
    public float _range;
    public float _rof;
    public ShootingMode _shootingMode;
    public WeaponType _weaponType; //for visual selection
    public string _currentWeaponShootAnim;
    public int _weaponSlot;

    public PlayerWeapon _weapon; //for coding
}
