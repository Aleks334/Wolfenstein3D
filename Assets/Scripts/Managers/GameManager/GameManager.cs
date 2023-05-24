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
    }

    private void ResetPlayerStats()
    {
        _healthManager.Data.GiveDefaultHealth();
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

        _onLoadScene.RaiseEvent(database.OnLossScenes, false);
        ResetPlayerStats();
        _lifesManager.Data.GiveDefaultLifes();
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
        
        _onLoadScene.RaiseEvent(new GameSceneData[] { database.SelectedEpisode, database.UI }, false);
        ResetPlayerStats();
        _lifesManager.RemoveLifes(1);
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

        _onLoadScene.RaiseEvent(database.OnVictoryScenes, false);

#if UNITY_EDITOR
        ResetPlayerStats();
        _lifesManager.Data.GiveDefaultLifes();
#endif
    }
}