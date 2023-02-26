using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player
    Transform PlayerSpawnPoint;
    GameObject playerAsset;

    public static GameObject PlayerObj { get; private set; }

    private HealthManager _healthManager;
    private AmmoManager _ammoManager;
    private LifesManager _lifesManager;

    //Menu data for import settings
    [SerializeField] ScenesData database;

    [Header("Event Channels")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;
    [SerializeField] private VoidEventChannelSO _onPlayerVictory;
    [SerializeField] private StringEventChannelSO _onChangingLevel;

    void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _onPlayerDeath.OnEventRaised += OnPlayerDeath;
        _onGameOver.OnEventRaised += OnGameOver;
        _onPlayerVictory.OnEventRaised += OnPlayerVictory;
    }

    private void OnDisable()
    {
        _onPlayerDeath.OnEventRaised -= OnPlayerDeath;
        _onGameOver.OnEventRaised -= OnGameOver;
        _onPlayerVictory.OnEventRaised -= OnPlayerVictory;
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

        if (_healthManager.Data._justDied)
        {
            _healthManager.Data.GiveDefaultHealth();
            _lifesManager.RemoveLifes(1);
            PlayerObj.GetComponent<PlayerWeaponManager>().DefaultWeapons();
            _ammoManager.Data.ResetAmmo();
            _healthManager.Data._justDied = false;
        }

        Debug.Log("Current Health: " + _healthManager.Data.playerHealth.CurrentHealth); 
        Debug.Log(
        " <b>CLICK TO VIEW SUMMARY OF INITIALIZATION -> </b>\n\n" +
        "--------------------------------\n" +
        "Current Episode: <b>" + database.SelectedEpisode + "</b>\n" +
        "--------------------------------\n" +
        "Current Difficulty Level: <b>" + database.DifficultyLvl + "</b>\n" +
        "--------------------------------\n");
    }
    
    void RestoreDefaultPlayerSettings()
    {
        Debug.LogWarning("RestoreDefaultPlayerSettings");
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

    private void OnPlayerVictory()
    {
        StartCoroutine(HandlePlayerVictory());
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
        _onChangingLevel.RaiseEvent(database.menuTabs[0].sceneName);
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

        _healthManager.Data._justDied = true;
        _onChangingLevel.RaiseEvent(database.episodes[(int)database.SelectedEpisode].sceneName);    
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
        _onChangingLevel.RaiseEvent(database.menuTabs[0].sceneName);
    }
}