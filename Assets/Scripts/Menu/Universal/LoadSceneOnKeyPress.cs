using UnityEngine;

public class LoadSceneOnKeyPress : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadSceneEventChannel;
    [SerializeField] private ScenesData _database;
    private Episode _episode;

    private void Start()
    {
        _episode = _database.SelectedEpisode;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _loadSceneEventChannel.RaiseEvent(new GameSceneData[] { _episode.CurrentFloor, _database.UI }, false);
        }          
    }
}