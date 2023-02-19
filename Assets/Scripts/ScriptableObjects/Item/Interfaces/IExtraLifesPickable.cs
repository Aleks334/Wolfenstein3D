public interface IExtraLifesPickable
{
    protected int ExtraLifesAmount { get; set; }

    protected void PickUpExtraLifes(PlayerStats playerStats);
}