using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneDB", menuName = "Game_Data/Database")]
public class ScenesData : ScriptableObject
{
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
}