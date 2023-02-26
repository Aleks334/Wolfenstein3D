using UnityEngine;

public class HealthManager : MonoBehaviour
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
            Debug.LogWarning("Event player died raised");
        }
        else if (!Data.isAlive() && _lifesData.CurrentLifes == 0)
        {
            _onGameOver.RaiseEvent();
            Debug.LogWarning("Event gameOver raised");
        }
    }

    public void HealPlayer(int healing)
    {
        // if (playerData.playerHealth.CurrentHealth == 0)
        //     return;
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