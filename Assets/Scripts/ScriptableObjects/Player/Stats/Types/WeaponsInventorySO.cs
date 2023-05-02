using UnityEngine;

/// <summary>
/// SO for keeping track of player weapon inventory.
/// </summary>

[CreateAssetMenu(menuName = "Player/Stats/Weapons Inventory")]
public class WeaponsInventorySO : ScriptableObject
{
    public PlayerWeapon[] WeaponsInInventory;

    [SerializeField] private int _defaultAmount;

    private void OnEnable()
    {
        WeaponsInInventory = new PlayerWeapon[_defaultAmount];
    }
}