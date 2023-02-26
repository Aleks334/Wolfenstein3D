public interface IHealthPickable
{
    public int HealthAmount { get; set; }

    public void PickUpHealth();
}