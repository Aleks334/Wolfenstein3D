using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Player
    Transform PlayerSpawnPoint;
    GameObject playerAsset;

    public GameObject PlayerObj { get; private set; }

    private HealthManager _healthManager;
    private AmmoManager _ammoManager;
    private LifesManager _lifesManager;

    //SphereCollider triggerNoise;
    public PlayerNoiseLevel playerNoiseLevel;

    //Game State
    public GameState gameState;
    public static event Action<GameState> OnGameStateChanged;

    //Elevator lever
 //   Renderer ElevatorLever;
   // Material elevatorLeverEnabledMat;

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
      //  elevatorLeverEnabledMat = Resources.Load("Materials/ElevatorLeverMat") as Material;

        PlayerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        PlayerObj = Instantiate(playerAsset, PlayerSpawnPoint.position, Quaternion.Euler(0f, -90f, 0f));
        #region Getting all needed player stats managers
        if (PlayerObj.TryGetComponent<HealthManager>(out HealthManager healthManager))
            _healthManager = healthManager;
        else
            Debug.LogError("GameManager couldn't find PlayerHealthManager");


        if (PlayerObj.TryGetComponent<AmmoManager>(out AmmoManager ammoManager))
            _ammoManager = ammoManager;
        else
            Debug.LogError("GameManager couldn't find PlayerAmmoManager");


        if (PlayerObj.TryGetComponent<LifesManager>(out LifesManager lifesManager))
            _lifesManager = lifesManager;
        else
            Debug.LogError("GameManager couldn't find PlayerLifesManager");
        #endregion
      //  ElevatorLever = GameObject.FindGameObjectWithTag("ElevatorLever").GetComponent<Renderer>();
        Debug.Log("Current Health: " + _healthManager.Data.playerHealth.CurrentHealth); 
       // ScenesManager.LoadUIAsync(); //Create SO for methods of this type
        Debug.Log(
        " <b>CLICK TO VIEW SUMMARY OF INITIALIZATION --> </b>\n\n" +
        "=====================================================\n\n" +

        "--------------------------------\n" +
        "Current Episode: <b>" + database.SelectedEpisode + "</b>\n" +
        "--------------------------------\n" +
        "Current Difficulty Level: <b>" + database.DifficultyLvl + "</b>\n" +
        "--------------------------------\n");
    }

    void RestoreDefaultPlayerSettings()
    {
        PlayerObj.GetComponent<PlayerWeaponManager>().DefaultWeapons();
        _ammoManager.Data.ResetAmmo();
        _lifesManager.Data.DecreasePlayerLifes(1);
        _healthManager.Data.GiveMaxHealth();   
    }

    IEnumerator HandlePlayerGameOver()
    {
        _healthManager.enabled = false;
        _ammoManager.enabled = false;
        _lifesManager.enabled = false;

        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);
        //After death anim with game over text
        Debug.Log("HandlePlayerGameOver");
        RestoreDefaultPlayerSettings();
        ScenesManager.instance.LoadLevel(Scenes.floor_1); 
    }

    IEnumerator HandlePlayerLiveLose()
    {
        _healthManager.enabled = false;
        _ammoManager.enabled = false;
        _lifesManager.enabled = false;

        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);
        //After death anim
        Debug.Log("HandlePlayerLiveLose");
         RestoreDefaultPlayerSettings();
        ScenesManager.instance.LoadLevel(Scenes.floor_1);
    }

    IEnumerator HandlePlayerVictory()
    {
        _healthManager.enabled = false;
        _ammoManager.enabled = false;
        _lifesManager.enabled = false;

        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        //ElevatorLever.material = elevatorLeverEnabledMat;

        yield return new WaitForSeconds(2f);
        //After victory anim
        Debug.Log("HandlePlayerVictory"); 
        ScenesManager.instance.LoadLevel(0);
    }
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