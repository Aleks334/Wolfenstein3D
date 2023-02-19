using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item Data")]
public class WeaponItemSO : ItemDataSO, IWeaponPickable
{
    [Tooltip("Select desired weapon type for item.")]

    [SerializeField] private WeaponType _weaponType;


    public override void PickupItem(GameObject player)
    {
        PlayerWeaponManager weaponManager = player.GetComponent<PlayerWeaponManager>();

        PickUpWeapon(weaponManager);  
    }

    public override bool CanBePickedUp(GameObject player)
    {
        if (!player.TryGetComponent<PlayerWeaponManager>(out PlayerWeaponManager weaponManager))
        {
            Debug.LogError("Gracz nie ma dodanej klasy PlayerWeaponManager!");
            return false;
        }

        if (!weaponManager.HaveThatWeapon(_weaponType))
            return true;
        else
            return false;
    }

    public void PickUpWeapon(PlayerWeaponManager weaponManager)
    {
        weaponManager.GiveWeapon(_weaponType);
    }
}
