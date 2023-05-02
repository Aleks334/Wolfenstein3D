using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] private StringEventChannelSO _onChangingLevel;

    private void OnEnable()
    {
        _onChangingLevel.OnEventRaised += LoadLevel;
    }

    private void OnDisable()
    {
        _onChangingLevel.OnEventRaised -= LoadLevel;
    }

    private void LoadLevel(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
    /*
    public static void LoadUIAsync()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }*/
}