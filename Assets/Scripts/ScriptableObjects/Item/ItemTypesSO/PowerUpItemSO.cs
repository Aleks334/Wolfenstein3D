using UnityEngine;

[CreateAssetMenu(menuName = "Item/PowerUp_Data")]
public class PowerUpItemSO : ItemDataSO, IHealthPickable, IAmmoPickable, IExtraLifesPickable
{
    #region Fields and Properties

    private HealthManager _healthManager;
    private AmmoManager _ammoManager;
    private LifesManager _lifesManager;

    [Tooltip("Select desired health value for item.")]
    [Range(1f, 100f)]
    [SerializeField] private int _healthAmount;
    public int HealthAmount
    {
        get => _healthAmount;
        set => _healthAmount = value;
    }

    [Tooltip("Select desired ammo value for item.")]
    [SerializeField] private int _ammoAmount;
    public int AmmoAmount
    {
        get => _ammoAmount;
        set => _ammoAmount = value;
    }

    [Tooltip("Select desired lifes value for item.")]
    [SerializeField] private int _extraLifesAmount;
    public int ExtraLifesAmount
    {
        get => _extraLifesAmount; 
        set => _extraLifesAmount = value;
    }

    #endregion

    #region ItemDataSO methods implementation

    protected override void FindNeededManager()
    {
        //Debug.LogWarning("FindNeededManager called");

        if (GameManager.PlayerObj.TryGetComponent<HealthManager>(out HealthManager healthManager))
            _healthManager = healthManager;
        else
           Debug.LogError("Player doesn't have HealthManager class attached!");

        if (GameManager.PlayerObj.TryGetComponent<AmmoManager>(out AmmoManager ammoManager))
            _ammoManager = ammoManager;
        else
            Debug.LogError("Player doesn't have AmmoManager class attached!");

        if (GameManager.PlayerObj.TryGetComponent<LifesManager>(out LifesManager lifesManager))
            _lifesManager = lifesManager;
        else
            Debug.LogError("Player doesn't have LifesManager class attached!");
    }

    public override bool CanBePickedUp()
    {
        if(!_healthManager || !_ammoManager || !_lifesManager)
            FindNeededManager();
        //Even if player has maximum health and maximum ammo we can add 1 extra life. 
        return true;
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
        _healthManager.HealPlayer(HealthAmount);
    }

    public void PickUpAmmo()
    {
        _ammoManager.AddAmmo(AmmoAmount);
    }

    public void PickUpExtraLifes()
    {
        _lifesManager.AddLifes(ExtraLifesAmount);
    }
    #endregion
}