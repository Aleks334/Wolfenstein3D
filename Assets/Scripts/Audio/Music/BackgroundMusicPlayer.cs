using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadSceneChannel;
    [SerializeField] private VoidEventChannelSO _voidLoadSceneChannel;
    private AudioCue _audioCue;

    private void Start()
    {
        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            _audioCue = audioCue;
        else
            Debug.LogError("Background music player doesn't have an AudioCue!");
    }

    private void OnEnable()
    {
        _loadSceneChannel.OnSceneLoadingRequested += PlayBackgroundMusic;
    }

    private void OnDisable()
    {
        _loadSceneChannel.OnSceneLoadingRequested -= PlayBackgroundMusic;
    }

    private void PlayBackgroundMusic(GameSceneData[] scenesToLoad, bool showProgressBar)
    {

        if (scenesToLoad[0].BackgroundMusic == default)
            return;

        if (_audioCue.AudioData != scenesToLoad[0].BackgroundMusic)
        {
            _audioCue.AudioData = scenesToLoad[0].BackgroundMusic;
            Debug.LogWarning("_audioCue.AudioData != scenesToLoad[0].BackgroundMusiC");
            _voidLoadSceneChannel.RaiseEvent();
            _audioCue.PlayAudioCue(true);
        }
        
    }
}