public interface IAmmoPickable
{
    protected int AmmoAmount { get; set; }

    protected void PickUpAmmo(PlayerStats playerStats);
}