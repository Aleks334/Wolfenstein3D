using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons/New Weapon Data SO")]
public class WeaponSO : ScriptableObject
{
    [Header("Properties of weapon")]
    [SerializeField] private int _damage;
    public int Damage
    { 
        get { return _damage; }
    }

    [SerializeField] private float _range;
    public float Range
    {
        get { return _range;  }
    }

    [SerializeField] private float _rof;
    public float Rof
    {
        get { return _rof;  }
    }

    [SerializeField] private ShootingMode _attackMode;
    public ShootingMode AttackMode
    {
        get { return _attackMode; }
    }

    [SerializeField] private WeaponType _weaponType;
    public WeaponType WeaponType
    {
        get { return _weaponType; }
    }

    [SerializeField] private string _currentWeaponShootAnim;
    public string CurrentWeaponShootAnim
    {
        get { return _currentWeaponShootAnim; }
    }

    [SerializeField] private int _weaponSlot;
    public int WeaponSlot
    {
        get { return _weaponSlot; }
    }

    [Header("Appearance of weapon")]
    public Sprite WeaponSprite;
    public AnimatorController WeaponAnimatorController;
}