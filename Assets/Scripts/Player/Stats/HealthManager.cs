using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthSO _data;
    [SerializeField] private PlayerLifeSO _lifesData;

    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;
    public HealthSO Data
    {
        get => _data;
    }

    private void OnEnable()
    {
        _onPlayerDeath.OnEventRaised += Data.ResetHealth;
        _onGameOver.OnEventRaised += Data.ResetHealth;
    }

    private void OnDisable()
    {
        _onPlayerDeath.OnEventRaised -= Data.ResetHealth;
        _onGameOver.OnEventRaised -= Data.ResetHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!Data.IsAlive())
            return;

        Data.HealthData.DmgValue(amount);

        UI.ReloadUI();
        UI.healthdecreaseeffect();

        //For raising events when player dies (depending on lifes number).
        if (!Data.IsAlive() && _lifesData.CurrentLifes > 0)
            _onPlayerDeath.RaiseEvent();
        else if (!Data.IsAlive() && _lifesData.CurrentLifes == 0)
            _onGameOver.RaiseEvent();
    }

    public void HealPlayer(int healing)
    {
        Data.HealthData.HealingValue(healing);
        UI.ReloadUI();
        UI.healtincreaseeffect();
    }

    public bool CanPickUpHealth()
    {
        if (Data.HealthData.CurrentHealth == Data.HealthData.MaxHealth)
            return false;
        else
            return true;
    }
}