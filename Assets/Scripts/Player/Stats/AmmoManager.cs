using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private PlayerAmmoSO _data;

    [Header("Event Channels")]
    [SerializeField] VoidEventChannelSO _playerDeathEventChannel;
    [SerializeField] VoidEventChannelSO _gameOverEventChannel;

    public PlayerAmmoSO Data
    {
        get => _data;
    }
    private void OnEnable()
    {
        _playerDeathEventChannel.OnEventRaised += Data.ResetAmmo;
        _gameOverEventChannel.OnEventRaised += Data.ResetAmmo;
    }

    private void OnDisable()
    {
        _playerDeathEventChannel.OnEventRaised -= Data.ResetAmmo;
        _gameOverEventChannel.OnEventRaised -= Data.ResetAmmo;
    }

    private void Start()
    {
        UI.ReloadUI(Data.CurrentAmmo);
    }
    
    public void AddAmmo(int ammoToAdd)
    {
        Data.AddAmmo(ammoToAdd);
        UI.ReloadUI(Data.CurrentAmmo);
    }
    public void RemoveAmmo()
    {
        Data.RemoveAmmo();
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
