using UnityEngine;

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
        for (int i = 0; i < scenesToLoad.Length; i++)
        {
            if (scenesToLoad[i].BackgroundMusic == default)
                continue;

            if (_audioCue.AudioData != scenesToLoad[i].BackgroundMusic)
            {
                _audioCue.AudioData = scenesToLoad[i].BackgroundMusic;
                _voidLoadSceneChannel?.RaiseEvent();

                _audioCue.PlayAudioCue(true);
            }
        }
    }
}