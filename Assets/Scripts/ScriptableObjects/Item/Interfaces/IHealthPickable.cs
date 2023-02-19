public interface IHealthPickable
{
    protected int HealthAmount { get; set; }

    protected void PickUpHealth(PlayerStats playerStats);
}