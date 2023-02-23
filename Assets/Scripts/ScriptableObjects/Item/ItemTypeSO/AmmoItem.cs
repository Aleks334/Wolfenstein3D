using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ammo_Data")]
public class AmmoItem : ItemDataSO, IAmmoPickable
{
    #region Fields and Properties

    private PlayerStats _statsManager;

    [Tooltip("Select desired ammo value for item.")]
    [SerializeField] private int _ammoAmount;

    public int AmmoAmount
    {
        get { return _ammoAmount; }

        set { _ammoAmount = value; }
    }

    #endregion

    #region ItemDataSO methods implementation

    protected override void FindNeededManager()
    {
        if (GameManager.Instance.PlayerObj.TryGetComponent<PlayerStats>(out PlayerStats statsManager))
        {
            //Debug.LogWarning("Znaleziono PlayerStats (ammo)!");
            _statsManager = statsManager;
        }
        else
        {
            Debug.LogError("Gracz nie ma dodanej klasy PlayerStats!");
        }
    }

    public override bool CanBePickedUp()
    {
        if (_statsManager == null)
        {
            FindNeededManager();
        }
            

        if (_statsManager.CanPickUpItem(true, false))
            return true;
        else
            return false;
    }

    public override void PickupItem()
    {
        PickUpAmmo();
    }
    #endregion

    public void PickUpAmmo()
    {
        _statsManager.AddAmmo(AmmoAmount);
    }
}
