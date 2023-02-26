using UnityEngine;

public class HealthManager : MonoBehaviour, IPlayerProfile
{
    [SerializeField] private PlayerHealthSO _data;
    [SerializeField] private PlayerLifeSO _lifesData;

    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;
    public PlayerHealthSO Data
    {
        get { return _data; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            DamagePlayer(12);
        else if (Input.GetKeyDown(KeyCode.H))
            HealPlayer(13);
    }
    
    public void DamagePlayer(int dmg)
    {
        Data.playerHealth.DmgValue(dmg);
        Debug.Log("Obecny poziom zdrowia: " + Data.playerHealth.CurrentHealth);
        UI.ReloadUI();
        UI.healthdecreaseeffect();

        //For raising events when player die and have lifes or not.
        if (!Data.isAlive() && _lifesData.CurrentLifes > 0)
        {
            _onPlayerDeath.RaiseEvent();
        }
        else if (!Data.isAlive() && _lifesData.CurrentLifes == 0)
        {
            _onGameOver.RaiseEvent();
        }
    }

    public void HealPlayer(int healing)
    {
        Data.playerHealth.HealingValue(healing);
        Debug.Log("Obecny poziom ¿ycia: " + Data.playerHealth.CurrentHealth);
        UI.ReloadUI();
        UI.healtincreaseeffect();
    }

    public bool CanPickUpHealth()
    {
        if (Data.playerHealth.CurrentHealth == Data.playerHealth.MaxHealth)
            return false;
        else
            return true;
    }
}