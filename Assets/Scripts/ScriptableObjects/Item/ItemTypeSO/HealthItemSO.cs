using UnityEngine;

[CreateAssetMenu(menuName = "Item/Health_Data")]
public class HealthItemSO : ItemDataSO, IHealthPickable
{
    #region Fields and Properties

    private PlayerStats _statsManager;

    [Tooltip("Select desired health value for item.")]
    [Range(1f, 100f)]
    [SerializeField] private int _healthAmount;

    public int HealthAmount
    {
        get { return _healthAmount; }

        set { _healthAmount = value; }
    }
    #endregion

    #region ItemDataSO methods implementation

    protected override void FindNeededManager()
    {
        if (GameManager.Instance.PlayerObj.TryGetComponent<PlayerStats>(out PlayerStats statsManager))
        {
            //Debug.LogWarning("Znaleziono PlayerStats! (health)");
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
            FindNeededManager();

        if (_statsManager.CanPickUpItem(false, true))
            return true;
        else
            return false;
    }

    public override void PickupItem()
    {
        PickUpHealth();
    }

    #endregion

    public void PickUpHealth()
    {
        _statsManager.HealPlayer(HealthAmount);
    }
}