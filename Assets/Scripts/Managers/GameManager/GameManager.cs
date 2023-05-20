using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Transform PlayerSpawnPoint;
    GameObject playerAsset;
    public static GameObject PlayerObj { get; private set; }

    private HealthManager _healthManager;
    private AmmoManager _ammoManager;
    private LifesManager _lifesManager;

    [SerializeField] ScenesData database;

    [Header("Game State Event Channels")]
    [SerializeField] private VoidEventChannelSO _onPlayerDeath;
    [SerializeField] private VoidEventChannelSO _onGameOver;
    [SerializeField] private VoidEventChannelSO _onPlayerVictory;

    [Header("Scene Load Event Channel")]
    [SerializeField] private LoadSceneEventChannelSO _onLoadScene;

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
        playerAsset = Resources.Load("Player/Player") as GameObject;
        PlayerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform;
        PlayerObj = Instantiate(playerAsset, PlayerSpawnPoint.position, Quaternion.Euler(0f, -90f, 0f));

       // _onLoadScene.RaiseEvent(database.UI, false, false, false);

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

        if (_healthManager.Data.JustDied)
        {
            _healthManager.Data.GiveDefaultHealth();
            _lifesManager.RemoveLifes(1);
            _ammoManager.Data.ResetAmmo();
            _healthManager.Data.JustDied = false;
        }
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
        _onLoadScene.RaiseEvent(database.OnLossScenes, false);
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

        _healthManager.Data.JustDied = true;
        
       _onLoadScene.RaiseEvent(new GameSceneData[] { database.SelectedEpisode, database.UI }, false);
    }

    IEnumerator HandlePlayerVictory()
    {
        _healthManager.enabled = false;
        _ammoManager.enabled = false;
        _lifesManager.enabled = false;

        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        //After victory anim
        Debug.Log("HandlePlayerVictory");
        _onLoadScene.RaiseEvent(database.OnVictoryScenes, false);
    }
}