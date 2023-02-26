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

    //OLD
    public PlayerNoiseLevel playerNoiseLevel;

    //Game State
    public GameState gameState;
    public static event Action<GameState> OnGameStateChanged;

    //Menu data for import settings
    [SerializeField] ScenesData database;

    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;

    void Awake()
    {
        
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Init();
        //  UpdateGameState(GameState.Init);
    }

    private void OnEnable()
    {
        _onPlayerDeath.OnEventRaised += OnPlayerDeath;
        _onGameOver.OnEventRaised += OnGameOver;
    }

    private void OnDisable()
    {
        _onPlayerDeath.OnEventRaised -= OnPlayerDeath;
        _onGameOver.OnEventRaised -= OnGameOver;
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
              //  OnGameStateChanged?.Invoke(newState);
             //   StartCoroutine(HandlePlayerLifeLoss());
                break;
            case GameState.GameOver:
             //   OnGameStateChanged?.Invoke(newState);
             //   StartCoroutine(HandlePlayerGameOver());
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

        if(database.episodes[(int)database.SelectedEpisode]._preload)
        {
            RestoreDefaultPlayerSettings();
            database.episodes[(int)database.SelectedEpisode]._preload = false;
        }

        if (_healthManager.Data._justDied)
        {
            
            _healthManager.Data.GiveDefaultHealth();
            _lifesManager.RemoveLifes(1);
            PlayerObj.GetComponent<PlayerWeaponManager>().DefaultWeapons();
            _ammoManager.Data.ResetAmmo();
            _healthManager.Data._justDied = false;
        }

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
        _lifesManager.Data.GiveDefaultLifes();
        _healthManager.Data.GiveDefaultHealth();
        PlayerObj.GetComponent<PlayerWeaponManager>().DefaultWeapons();
        _ammoManager.Data.ResetAmmo();
    }
   
    

    private void OnPlayerDeath()
    {
        StartCoroutine(HandlePlayerLifeLoss());
    }

    private void OnGameOver()
    {
        StartCoroutine(HandlePlayerGameOver());
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
        //  RestoreDefaultPlayerSettings();

        //_lifesManager.RemoveLifes(1);
        ScenesManager.instance.LoadLevel(database.menuTabs[0].sceneName);
    }

    IEnumerator HandlePlayerLifeLoss()
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
        // RestoreDefaultPlayerSettings();

        _healthManager.Data._justDied = true;
        ScenesManager.instance.LoadLevel(database.episodes[(int)database.SelectedEpisode].sceneName);
        
    }

    IEnumerator HandlePlayerVictory()
    {
        _healthManager.enabled = false;
        _ammoManager.enabled = false;
        _lifesManager.enabled = false;

        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);
        //After victory anim
        Debug.Log("HandlePlayerVictory");
        ScenesManager.instance.LoadLevel(database.menuTabs[1].sceneName);
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