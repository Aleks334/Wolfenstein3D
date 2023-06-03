using UnityEngine;

public class LifesManager : MonoBehaviour
{
    [SerializeField] private PlayerLifeSO _data;

    [Header("Event Channels")]
    [SerializeField] VoidEventChannelSO _playerDeathEventChannel;
    [SerializeField] VoidEventChannelSO _gameOverEventChannel;

    public PlayerLifeSO Data
    {
        get => _data;
    }

    private void OnEnable()
    {
        _playerDeathEventChannel.OnEventRaised += TakeLife;
        _gameOverEventChannel.OnEventRaised += Data.ResetLifes;
    }

    private void OnDisable()
    {
        _playerDeathEventChannel.OnEventRaised -= TakeLife;
        _gameOverEventChannel.OnEventRaised -= Data.ResetLifes;
    }

    public void AddLifes(int lifesToAdd)
    {
        Data.IncreasePlayerLifes(lifesToAdd);
        UI.ReloadUI();
    }
    public void RemoveLifes(int lifesToRemove)
    {
        Data.DecreasePlayerLifes(lifesToRemove);
        UI.ReloadUI();
    }

    private void TakeLife()
    {
        RemoveLifes(1);
    }
}
