using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons/New Weapon Data SO")]
public class WeaponSO : ScriptableObject
{
    [Header("Properties of weapon")]
    [SerializeField] private int _damage;
    public int Damage
    {
        get => _damage;
    }

    [SerializeField] private float _range;
    public float Range
    {
        get => _range;
    }

    [SerializeField] private float _rof;
    public float Rof
    {
        get => _rof;
    }

    [SerializeField] private ShootingMode _attackMode;
    public ShootingMode AttackMode
    {
        get => _attackMode;
    }

    [SerializeField] private WeaponType _weaponType;
    public WeaponType WeaponType
    {
        get => _weaponType;
    }

    [SerializeField] private int _weaponSlot;
    public int WeaponSlot
    {
        get => _weaponSlot;
    }


    [Header("Appearance of weapon")]
    [SerializeField] private AnimationClip _weaponAttackAnim;
    public string WeaponAttackAnim
    {
        get => _weaponAttackAnim.name;
    }

    public Sprite WeaponSprite;
    public AnimatorController WeaponAnimatorController;

    [Header("Sound for weapon attack")]
    [SerializeField] private AudioCueSO _audioCueSO;
    public AudioCueSO WeaponAttackSound
    {
        get => _audioCueSO;
    }
}