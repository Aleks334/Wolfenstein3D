using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ammo_Data")]
public class AmmoItem : ItemDataSO, IAmmoPickable
{
    #region Fields and Properties

    private AmmoManager _ammoManager;

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
        if (GameManager.Instance.PlayerObj.TryGetComponent<AmmoManager>(out AmmoManager ammoManager))
        {
            //Debug.LogWarning("Znaleziono PlayerStats (ammo)!");
            _ammoManager = ammoManager;
        }
        else
        {
            Debug.LogError("Gracz nie ma dodanej klasy PlayerStats!");
        }
    }

    public override bool CanBePickedUp()
    {
        if (_ammoManager == null)
        {
            FindNeededManager();
        }
            

        if (_ammoManager.CanPickUpAmmo())
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
        _ammoManager.AddAmmo(AmmoAmount);
    }
}
