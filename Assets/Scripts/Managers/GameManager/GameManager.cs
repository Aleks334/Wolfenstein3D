using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Transform PlayerSpawnPoint;
    GameObject playerAsset;
    public static GameObject PlayerObj { get; private set; }

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
        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);

        _onLoadScene.RaiseEvent(database.OnLossScenes, false);
    }

    IEnumerator HandlePlayerLifeLoss()
    {
        var episode = database.SelectedEpisode;

        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(2f);
        
        _onLoadScene.RaiseEvent(new GameSceneData[] { episode.CurrentFloor, database.UI }, false);
    }

    IEnumerator HandlePlayerVictory()
    {
        PlayerObj.GetComponent<PlayerMovementManager>().enabled = false;
        PlayerObj.GetComponent<PlayerWeaponManager>().enabled = false;
        PlayerObj.GetComponent<RaycastInteractionController>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        database.SelectedEpisode.GoToNextFloor();
        _onLoadScene.RaiseEvent(database.OnVictoryScenes, false);
    }
}