using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon_Data")]
public class WeaponItemSO : ItemDataSO, IWeaponPickable
{
    #region Fields and Properties

    [Tooltip("Select desired weapon type for item.")]
    [SerializeField] private WeaponType _weaponType;

    private PlayerWeaponManager _weaponManager;

    #endregion

    #region ItemDataSO methods implementation

    public override void PickupItem()
    {
        PickUpWeapon();  
    }

    protected override void FindNeededManager()
    {
        if (GameManager.Instance.PlayerObj.TryGetComponent<PlayerWeaponManager>(out PlayerWeaponManager weaponManager))
        {
            _weaponManager = weaponManager;
           // Debug.LogWarning("Znaleziono PlayerWeaponManager!");
        }
        else
        {
            Debug.LogError("Gracz nie ma dodanej klasy PlayerWeaponManager!");
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