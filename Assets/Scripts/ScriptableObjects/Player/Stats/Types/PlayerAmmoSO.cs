using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats/Ammo_Stats")]
public class PlayerAmmoSO : ScriptableObject
{
    [SerializeField] private int _defaultAmount;
    [SerializeField] private int _maxAmount;

    public int CurrentAmmo { get; private set; }
    public int MaxAmmo { get; private set; }


    private void OnEnable()
    {
        CurrentAmmo = _defaultAmount;
        MaxAmmo = _maxAmount;
    }

    //Resets ammo on game init
    public void ResetAmmo()
    {
        CurrentAmmo = _defaultAmount;
    }

    public void AddAmmo(int ammoToAdd)
    {
        if (CurrentAmmo < MaxAmmo)
        {
            CurrentAmmo += ammoToAdd;

            if (CurrentAmmo > MaxAmmo)
                CurrentAmmo = MaxAmmo;
        }
    }

    public void RemoveAmmo()
    {
        if (CurrentAmmo > 0)
            CurrentAmmo--;

        if (CurrentAmmo <= 0)
            CurrentAmmo = 0;
    }
}