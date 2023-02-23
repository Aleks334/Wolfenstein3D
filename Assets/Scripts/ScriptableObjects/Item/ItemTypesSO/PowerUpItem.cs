using UnityEngine;

[CreateAssetMenu(menuName = "Item/PowerUp_Data")]
public class PowerUpItem : ItemDataSO, IHealthPickable, IAmmoPickable, IExtraLifesPickable
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

    [Tooltip("Select desired ammo value for item.")]
    [SerializeField] private int _ammoAmount;
    public int AmmoAmount
    {
        get { return _ammoAmount; }

        set { _ammoAmount = value; }
    }

    [Tooltip("Select desired lifes value for item.")]
    [SerializeField] private int _extraLifesAmount;
    public int ExtraLifesAmount
    {
        get { return _extraLifesAmount; }

        set { _extraLifesAmount = value; }
    }

    #endregion

    #region ItemDataSO methods implementation

    protected override void FindNeededManager()
    {
        if (GameManager.Instance.PlayerObj.TryGetComponent<PlayerStats>(out PlayerStats statsManager))
        {
            //Debug.LogWarning("Znaleziono PlayerStats! (power up)");
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

        if (_statsManager.CanPickUpPowerUp())
            return true;
        else
            return false;
    }

    public override void PickupItem()
    {
        PickUpHealth();
        PickUpAmmo();
        PickUpExtraLifes();
    }
    #endregion

    #region methods of interfaces implementation
    public void PickUpHealth()
    {
        _statsManager.HealPlayer(HealthAmount);
    }

    public void PickUpAmmo()
    {
        _statsManager.AddAmmo(AmmoAmount);
    }

    public void PickUpExtraLifes()
    {
        _statsManager.AddLifes(ExtraLifesAmount);
    }
    #endregion
}