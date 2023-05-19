using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadSceneChannel;
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
            if (scenesToLoad[i].BackgroundMusic == null)
                continue;

            if (_audioCue.AudioData != scenesToLoad[i].BackgroundMusic)
            {
                _audioCue.AudioData = scenesToLoad[i].BackgroundMusic;
                _audioCue.PlayAudioCue();
            }
                
        }
    }
}