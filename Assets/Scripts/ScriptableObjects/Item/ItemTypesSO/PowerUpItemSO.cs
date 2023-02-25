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
        if (GameManager.Instance.PlayerObj.TryGetComponent<HealthManager>(out HealthManager healthManager))
            _healthManager = healthManager;
        else
            Debug.LogError("Gracz nie ma dodanej klasy HealthManager!");


        if (GameManager.Instance.PlayerObj.TryGetComponent<AmmoManager>(out AmmoManager ammoManager))
            _ammoManager = ammoManager;
        else
            Debug.LogError("Gracz nie ma dodanej klasy HealthManager!");


        if (GameManager.Instance.PlayerObj.TryGetComponent<LifesManager>(out LifesManager lifesManager))
            _lifesManager = lifesManager;
        else
            Debug.LogError("Gracz nie ma dodanej klasy HealthManager!");
    }

    public override bool CanBePickedUp()
    {
        if (!_healthManager || !_ammoManager || !_lifesManager)
            FindNeededManager();

        //Nawet jeœli gracz ma maks. zdrowie, maks. amunicjê to mo¿na dodaæ 1 ¿ycie.
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