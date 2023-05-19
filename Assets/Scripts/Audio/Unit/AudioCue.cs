using UnityEngine;

public class AudioCue : MonoBehaviour
{
    [Header("Sound definition")]
    [SerializeField] private AudioCueSO _audioData;
    public AudioCueSO AudioData
    {
        get => _audioData;
        set => _audioData = value;
    }

    [SerializeField] private bool _playOnStart = false;

    [Header("Sound Configuration")]
    [SerializeField] private AudioConfigurationSO _audioSettings;
    [SerializeField] private AudioCueEventChannelSO _audioCueEventChannel;

    private void Start()
    {
        if (_playOnStart)
            PlayAudioCue();
    }

    public void PlayAudioCue()
    {
        _audioCueEventChannel.RaiseEvent(_audioData, _audioSettings, transform.position);
    }
}
