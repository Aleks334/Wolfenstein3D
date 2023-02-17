using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class UI : MonoBehaviour
{

    public  TextMeshProUGUI healthtext;
    public  TextMeshProUGUI Lifecounttext;
    public static bool anim = false;
    static bool reload = false;

    public PlayerData playerData;

    //subscribe to event OnGameStateChange in order to affect on game state change.
    void Awake()
    {
        GameManager.OnGameStateChanged += LiveLose_OnGameStateChanged;
        GameManager.OnGameStateChanged += Victory_OnGameStateChanged;
        GameManager.OnGameStateChanged += GameOver_OnGameStateChanged;
        GameManager.OnGameStateChanged += Running_OnGameStateChanged; 
    }

    private void Start()
    {
        healthtext.text = playerData.playerHealth.CurrentHealth.ToString() + "%";
        Lifecounttext.text = playerData.playerLives.CurrentLives.ToString();
    }

    //unsubscribe to event OnGameStateChange when object is destroyed or after loading next scene.
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= LiveLose_OnGameStateChanged;
        GameManager.OnGameStateChanged -= Victory_OnGameStateChanged;
        GameManager.OnGameStateChanged -= GameOver_OnGameStateChanged;
        GameManager.OnGameStateChanged -= Running_OnGameStateChanged;
    }

    void Running_OnGameStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            HealthChangePanelScript.zeroHealth = false;
            HealthChangePanelScript.gameOverEffect = false;
            HealthChangePanelScript.victory = false;
        }
    }

    void LiveLose_OnGameStateChanged(GameState state)
    {
        if (state == GameState.LiveLose)
        {
            HealthChangePanelScript.zeroHealth = true;
            Debug.Log("Utrata 1 �ycia. Wy�wietlenie efektu panelu, kt�ry staje si� coraz bardziej czerwony");
        }
           
    }

    void GameOver_OnGameStateChanged(GameState state)
    {
        if (state == GameState.GameOver)
        {
            HealthChangePanelScript.zeroHealth = true;
            HealthChangePanelScript.gameOverEffect = true;
            Debug.Log("Wy�wietlenie efektu panelu jak wcze�niej, tylko z napisem w stylu GameOver");
        }
           
    }

    void Victory_OnGameStateChanged(GameState state)
    {
        if (state == GameState.Victory)
        {
            HealthChangePanelScript.victory = true;
            Debug.Log("Wy�wietlenie panelu dot. wygranej gracza");
        }
    }

    void Update()
    {
        anim = false;
        if(reload)
        {
            
            healthtext.text = playerData.playerHealth.CurrentHealth.ToString() + "%";
            Lifecounttext.text = playerData.playerLives.CurrentLives.ToString();
            anim = true;
            reload = false;
            WaeponScript.reload = true;
        }
    }

    public static void ReloadUI()
    {
        reload = true;

    }
    public static void ReloadUI(int ammocount)
    {
        reload = true;
        WaeponScript.ammoamount = ammocount.ToString();
    }
    public static void healtincreaseeffect()
    {
        HealthChangePanelScript.healthincrease = true;
    }
    public static void healthdecreaseeffect()
    {
        HealthChangePanelScript.healthdecrease = true;
    }
}