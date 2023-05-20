using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private PlayerAmmoSO _data;
    public PlayerAmmoSO Data
    {
        get => _data;
    }
    private void Start()
    {
        UI.ReloadUI(Data.CurrentAmmo);
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            AddAmmo(6);
    }
    */
    public void AddAmmo(int ammoToAdd)
    {
        Data.AddAmmo(ammoToAdd);
       // Debug.Log("Current Ammo: " + Data.CurrentAmmo);
        UI.ReloadUI(Data.CurrentAmmo);
    }
    public void RemoveAmmo()
    {
        Data.RemoveAmmo();
        // Debug.Log("Current Ammo: " + Data.CurrentAmmo);
        UI.ReloadUI(Data.CurrentAmmo);
    }

    public bool CanPickUpAmmo()
    {
        if (Data.CurrentAmmo == Data.MaxAmmo)
            return false;
        else
            return true;
    }
}
