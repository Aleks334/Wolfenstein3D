using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon_Data")]
public class WeaponItemSO : ItemDataSO, IWeaponPickable
{
    #region Fields and Properties

    [Tooltip("Select desired weapon type for item.")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private VoidEventChannelSO _specialEffectUI;

    private PlayerWeaponManager _weaponManager;

    #endregion

    #region ItemDataSO methods implementation

    public override void PickupItem()
    {
        PickUpWeapon();

        if (_specialEffectUI != null)
            _specialEffectUI.RaiseEvent();
    }

    protected override void FindNeededManager()
    {
        if (GameManager.PlayerObj.TryGetComponent<PlayerWeaponManager>(out PlayerWeaponManager weaponManager))
        {
            _weaponManager = weaponManager;
           // Debug.LogWarning("PlayerWeaponManager was found!");
        }
        else
        {
            Debug.LogError("Player doesn't have PlayerWeaponManager class attached!");
        }
    }

    public override bool CanBePickedUp()
    {

        if (_weaponManager == null)
            FindNeededManager();

        if (!_weaponManager.HaveThatWeapon(_weaponManager.ExistingWeaponsData.WeaponTypeToPlayerWeapon(_weaponType)))
            return true;
        else
            return false;
    }
    #endregion

    public void PickUpWeapon()
    {
        _weaponManager.GiveWeapon(_weaponManager.ExistingWeaponsData.WeaponTypeToPlayerWeapon(_weaponType));
    }
}