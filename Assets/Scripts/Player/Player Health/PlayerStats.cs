using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    bool _canPickUpAmmo;

    void Awake()
    {
        _canPickUpAmmo = true;  
    }

    void Start()
    {
        UI.ReloadUI(playerData.playerAmmo.CurrentAmmo);
    }

    void Update()
    {
        // TESTS OF HEALTH SYSTEM
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            DamagePlayer(12);
            GameObject e;
            e = GameObject.FindGameObjectWithTag("Enemy");
            e.GetComponent<EnemyManager>().dmgenemy(30);
        }
        else if (Input.GetKeyDown(KeyCode.H))
            HealPlayer(13);

        // TESTS OF LIVES SYSTEM
        if (Input.GetKeyDown(KeyCode.L))
            AddLives(1);
        else if(Input.GetKeyDown(KeyCode.J))
            RemoveLives(1);

        // TESTS OF ADDING AMMO
       if (Input.GetKeyDown(KeyCode.P))
            AddAmmo(2);  */
    }

    // -------------  Public methods for damaging/healing player by other objects --------------
    // HEALTH Methods
    public void DamagePlayer(int dmg)
    {

        playerData.playerHealth.DmgValue(dmg);
        Debug.Log("Obecny poziom ¿ycia: " + playerData.playerHealth.CurrentHealth);
        UI.ReloadUI();
        UI.healthdecreaseeffect();

        //Check if player has 0 health and at least one live
        if(playerData.playerHealth.CurrentHealth == 0 && playerData.playerLives.CurrentLives > 0)
        {
            GameManager.instance.UpdateGameState(GameState.LiveLose);
            Debug.Log("UpdateGameState(GameState.LiveLose)");

        } else if(playerData.playerHealth.CurrentHealth == 0 && playerData.playerLives.CurrentLives == 0)
        {
            GameManager.instance.UpdateGameState(GameState.GameOver);
            Debug.Log("UpdateGameState(GameState.GameOver)");
        }
    }

    public void HealPlayer(int healing)
    {
        if (playerData.playerHealth.CurrentHealth == 0)
            return;
        playerData.playerHealth.HealingValue(healing);
        Debug.Log("Obecny poziom ¿ycia: " + playerData.playerHealth.CurrentHealth);
        UI.ReloadUI();
        UI.healtincreaseeffect();
    }
    // LIVES Methods
    public void AddLives(int livesToAdd)
    {

        playerData.playerLives.IncreasePlayerLives(livesToAdd);
        Debug.Log("Obecny poziom ¿ycia: " + playerData.playerLives.CurrentLives);
        UI.ReloadUI();
    }
    public void RemoveLives(int livesToRemove)
    {

        playerData.playerLives.DecreasePlayerLives(livesToRemove);
        Debug.Log("Obecny poziom ¿ycia: " + playerData.playerLives.CurrentLives);
        UI.ReloadUI();
    }

    //AMMO Methods
    public void AddAmmo(int ammoToAdd)
    {
        if (_canPickUpAmmo)
        {
            playerData.playerAmmo.AddAmmo(ammoToAdd);
            Debug.Log("Obecny stan amunicji: " + playerData.playerAmmo.CurrentAmmo);
            UI.ReloadUI(playerData.playerAmmo.CurrentAmmo);
        }
        else
            Debug.Log("Player has full ammo. He can't add more");
    }
    public void RemoveAmmo()
    {
        playerData.playerAmmo.RemoveAmmo();
            Debug.Log("Obecny stan amunicji: " + playerData.playerAmmo.CurrentAmmo);
            UI.ReloadUI(playerData.playerAmmo.CurrentAmmo);
    }
    /*
    public int GetCurrentAmmo()
    {
        return playerAmmo.CurrentAmmo;
    }*/
    public bool CanPickUpItem(bool checkAmmo, bool checkHealth, bool checkForPowerUp = false)
    {
        if (checkAmmo)
        {
            if (playerData.playerAmmo.CurrentAmmo == playerData.playerAmmo.MaxAmmo)
                return false;
            else
                return true;
        }
        else if (checkHealth)
        {
            if (playerData.playerHealth.CurrentHealth == playerData.playerHealth.MaxHealth)
                return false;
            else
                return true;
        }
        else if (checkForPowerUp)
            return true; //Nawet jeœli gracz ma maks. zdrowie, maks. amunicjê to mo¿na dodaæ 1 ¿ycie.
        else
        {
            return false;
        }
           
    }
}
