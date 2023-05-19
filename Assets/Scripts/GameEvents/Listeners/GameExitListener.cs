using UnityEngine;

public class GameExitListener : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _exitEventChannel;

    private void OnEnable()
    {
        _exitEventChannel.OnEventRaised += OnExitGame;
    }

    private void OnDisable()
    {
        _exitEventChannel.OnEventRaised -= OnExitGame;
    }

    void OnExitGame()
    {
        Application.Quit();
        Debug.LogWarning("QUIT GAME");
    }
}