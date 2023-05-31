using UnityEngine;

public class GameLoaderButton : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadMenuTabEventChannel;
    [SerializeField] private ScenesData _database;

    [SerializeField] private bool _showProgressBar;

    public void LoadScene()
    {
        _loadMenuTabEventChannel.RaiseEvent(new GameSceneData[] { _database.SelectedEpisode, _database.UI }, _showProgressBar);
    }
}