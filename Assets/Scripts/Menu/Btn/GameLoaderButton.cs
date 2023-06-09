using UnityEngine;

public class GameLoaderButton : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadMenuTabEventChannel;
    [SerializeField] private ScenesData _database;

    [SerializeField] private bool _showProgressBar;

    [Header("Player Stats")]
    [SerializeField] private HealthSO _playerHealth;
    [SerializeField] private PlayerLifeSO _playerlifes;
    [SerializeField] private PlayerAmmoSO _playerAmmo;

    public void LoadScene()
    {
        var episode = _database.SelectedEpisode;

        _database.SelectedEpisode.SetFloorOnInit();

        _loadMenuTabEventChannel.RaiseEvent(new GameSceneData[] { episode.CurrentFloor, _database.UI }, _showProgressBar);
    }

    public void ResetAllPlayerStats()
    {
        _playerHealth.ResetHealth();
        _playerAmmo.ResetAmmo();
        _playerlifes.ResetLifes();
    }
}