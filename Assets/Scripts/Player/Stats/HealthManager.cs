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

    public void TakeDamage(int amount)
    {
        Data.HealthData.DmgValue(amount);
        //Debug.Log("Current health: " + Data.playerHealth.CurrentHealth);
        UI.ReloadUI();
        UI.healthdecreaseeffect();

        //For raising events when player dies (depending on lifes number).
        if (!Data.isAlive() && _lifesData.CurrentLifes > 0)
            _onPlayerDeath.RaiseEvent();
        else if (!Data.isAlive() && _lifesData.CurrentLifes == 0)
            _onGameOver.RaiseEvent();
    }

    public void HealPlayer(int healing)
    {
        Data.HealthData.HealingValue(healing);
      //  Debug.Log("Current health: " + Data.playerHealth.CurrentHealth);
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