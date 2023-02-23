using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Player
    Transform PlayerSpawnPoint;
    GameObject playerAsset;

    public GameObject PlayerObj { get; private set; }
    public PlayerStats playerStats;
    [SerializeField] PlayerData playerData;
    SphereCollider triggerNoise;
    public PlayerNoiseLevel playerNoiseLevel;

    //Game State
    public GameState gameState;
    public static event Action<GameState> OnGameStateChanged;

    //Elevator lever
    Renderer ElevatorLever;
    Material elevatorLeverEnabledMat;

    //Menu data for import settings
    [SerializeField] ScenesData database;


    void Awake()
    {
        
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        UpdateGameState(GameState.Init);
    }

    private void Start()
    {
        UpdateGameState(GameState.Running);
    }

    // Methods for managing game states.
    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Init:
                Init();
                break;
            case GameState.Running:
                OnGameStateChanged?.Invoke(newState);
                break;
            case GameState.LiveLose:
                OnGameStateChanged?.Invoke(newState);
                StartCoroutine(HandlePlayerLiveLose());
                break;
            case GameState.GameOver:
                OnGameStateChanged?.Invoke(newState);
                StartCoroutine(HandlePlayerGameOver());
                break;
            case GameState.Victory:
                StartCoroutine(HandlePlayerVictory());
                OnGameStateChanged?.Invoke(newState);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void Init()
    {
        database.UpdateMenuPage(MenuPage.InGame);
        playerAsset = Resources.Load("Player/Player") as GameObject;
        elevatorLeverEnabledMat = Resources.Load("Materials/ElevatorLeverMat") as Material;

        PlayerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        PlayerObj = Instantiate(playerAsset, PlayerSpawnPoint.position, Quaternion.Euler(0f, -90f, 0f));
        // = GameObject.FindGameObjectWithTag("Player");
        playerStats = PlayerObj.GetComponent<PlayerStats>();
        //triggerNoise = playerObj.GetComponent<SphereCollider>();
      //  triggerNoise.isTrigger = true;
        ElevatorLever = GameObject.FindGameObjectWithTag("ElevatorLever").GetComponent<Renderer>();
        Debug.Log("Current Health: " + playerData.playerHealth.CurrentHealth);
        // playerObj.GetComponent<PlayerStats>().HealPlayer(playerData.playerHealth.MaxHealth);
       // ScenesManager.LoadUIAsync(); //Create SO for methods of this type
        //Summary
        Debug.Log
        (
        " <b>CLICK TO VIEW SUMMARY OF INITIALIZATION --> </b>\n\n" +
        "=====================================================\n\n" +

        "--------------------------------\n" +
        "Current Episode: <b>" + database.SelectedEpisode + "</b>\n" +
        "--------------------------------\n" +
        "Current Difficulty Level: <b>" + database.DifficultyLvl + "</b>\n" +
        "--------------------------------\n"
        );
    }

    void RestoreDefaultPlayerSettings()
    {
        PlayerObj.GetComponent<PlayerWeaponManager>().DefaultWeapons();
        playerData.ResetAmmo();
        playerData.TakeLife();
        playerData.GiveMaxHealth();   
    }

  /*  void Update()
    {
      ChangePlayerNoiseLevel();
    }*/

    IEnumerator HandlePlayerGameOver()
    {
        PlayerObj.GetComponent<PlayerStats>().enabled = false;
        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
      //  PlayerObj.GetComponent<PlayerShooting>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);
        //After death anim with game over text
        Debug.Log("HandlePlayerGameOver");
        RestoreDefaultPlayerSettings();
        ScenesManager.instance.LoadLevel(Scenes.floor_1);
       // ScenesManager.instance.ResetAttemps();

        
    }

    IEnumerator HandlePlayerLiveLose()
    {
        PlayerObj.GetComponent<PlayerStats>().enabled = false;
        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
     //   PlayerObj.GetComponent<PlayerShooting>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);
        //After death anim
        Debug.Log("HandlePlayerLiveLose");
         RestoreDefaultPlayerSettings();
      //  ScenesManager.instance.PlayerLostLife(true);
        ScenesManager.instance.LoadLevel(Scenes.floor_1);
       
    }

    IEnumerator HandlePlayerVictory()
    {
        PlayerObj.GetComponent<PlayerStats>().enabled = false;
        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
    //    PlayerObj.GetComponent<PlayerShooting>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        ElevatorLever.material = elevatorLeverEnabledMat;

        yield return new WaitForSeconds(2f);
        //After victory anim
        Debug.Log("HandlePlayerVictory"); 
        ScenesManager.instance.LoadLevel(0);
    }
    /*
    public void ChangePlayerNoiseLevel()
    {
        //Changing sphere collider trigger radius on different level of player noise.
        switch ((int)playerNoiseLevel)
        {
            case (int)PlayerNoiseLevel.standing:
                triggerNoise.radius = (int)PlayerNoiseLevel.standing;
                break;
            case (int)PlayerNoiseLevel.walking:
                triggerNoise.radius = (int)PlayerNoiseLevel.walking;
                break;
            case (int)PlayerNoiseLevel.running:
                triggerNoise.radius = (int)PlayerNoiseLevel.running;
                break;
            case (int)PlayerNoiseLevel.shooting:
                triggerNoise.radius = (int)PlayerNoiseLevel.shooting;
                break;
            default:
                break;
        }
    }*/
}

public enum GameState
{
    Init,
    Running,
    LiveLose,
    GameOver,
    Victory,
}


public enum PlayerNoiseLevel
{
    standing = 12,
    walking = 16,
    running = 23,
    shooting = 35,
}