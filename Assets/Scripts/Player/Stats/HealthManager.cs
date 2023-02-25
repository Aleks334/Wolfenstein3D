using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private PlayerHealthSO _data;
    public PlayerHealthSO Data
    {
        get { return _data; }
    }
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            DamagePlayer(12);
        else if (Input.GetKeyDown(KeyCode.H))
            HealPlayer(13);
    }
    */
    public void DamagePlayer(int dmg)
    {

        Data.playerHealth.DmgValue(dmg);
        Debug.Log("Obecny poziom ¿ycia: " + Data.playerHealth.CurrentHealth);
        UI.ReloadUI();
        UI.healthdecreaseeffect();

        //Check if player has 0 health and at least one live
      /*  if (Data.playerHealth.CurrentHealth == 0 && Data.playerLifes.CurrentLifes > 0)
        {
            GameManager.Instance.UpdateGameState(GameState.LiveLose);
            Debug.Log("UpdateGameState(GameState.LiveLose)");

        }
        else if (Data.playerHealth.CurrentHealth == 0 && Data.playerLifes.CurrentLifes == 0)
        {
            GameManager.Instance.UpdateGameState(GameState.GameOver);
            Debug.Log("UpdateGameState(GameState.GameOver)");
        }*/
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