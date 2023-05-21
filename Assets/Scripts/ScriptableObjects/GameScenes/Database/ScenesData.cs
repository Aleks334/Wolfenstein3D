using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneDB", menuName = "Game_Data/Database")]
public class ScenesData : ScriptableObject
{
    public Episode[] episodes = new Episode[6];
    public List<MenuTab> menuTabs = new List<MenuTab>();

    public MaintenanceSceneSO UI;
    public MaintenanceSceneSO Init;
    public Episode SelectedEpisode { get; set; }
    public DifficultyLevelSO DifficultyLvl { get; set; }

    [SerializeField] private GameSceneData[] _onVictoryScenes;
    public GameSceneData[] OnVictoryScenes
    {
        get => _onVictoryScenes;
    }

    [SerializeField] private GameSceneData[] _onLossScenes;
    public GameSceneData[] OnLossScenes
    {
        get => _onLossScenes;
    }

    [Header("Load Scene Event Channel")]
    [SerializeField] private LoadSceneEventChannelSO _loadSceneChannel;

    [SerializeField] private GameSceneData _currentActiveScene;
    public GameSceneData CurrentActiveScene
    {
        get => _currentActiveScene;
    }

    public void SubscribeToLoadingSceneEvent()
    {
        _currentActiveScene = default;
        _loadSceneChannel.OnSceneLoadingRequested += UpdateCurrentScene;
    }

    private void UpdateCurrentScene(GameSceneData[] scenesToLoad, bool showProgressBar)
    {
        _currentActiveScene = scenesToLoad[0];
    }
}