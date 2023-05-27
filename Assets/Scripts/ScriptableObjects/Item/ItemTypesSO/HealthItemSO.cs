using UnityEngine;

[CreateAssetMenu(menuName = "Item/Health_Data")]
public class HealthItemSO : ItemDataSO, IHealthPickable
{
    #region Fields and Properties

    private HealthManager _healthManager;

    [Tooltip("Select desired health value for item.")]
    [Range(1f, 100f)]
    [SerializeField] private int _healthAmount;

    public int HealthAmount
    {
        get => _healthAmount;
        set => _healthAmount = value;
    }
    #endregion

    #region ItemDataSO methods implementation

    protected override void FindNeededManager()
    {
        if (GameManager.PlayerObj.TryGetComponent<HealthManager>(out HealthManager healthManager))
        {
            //Debug.LogWarning("HealthManager was found! (health)");
            _healthManager = healthManager;
        }
        else
        {
            Debug.LogError("Player doesn't have HealthManager class attached!");
        }
    }

    public override bool CanBePickedUp()
    {
        if (_healthManager == null)
            FindNeededManager();

        if (_healthManager.CanPickUpHealth())
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
        _healthManager.HealPlayer(HealthAmount);
    }
}