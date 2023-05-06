using UnityEngine;

[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio/Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    [Header("Looping works only when array includes single clip!")]
    public bool looping = false;
    [SerializeField] private PlaybackMode _sequenceMode = PlaybackMode.Sequential;
    public PlaybackMode SequenceMode 
    { 
        get { return _sequenceMode; }
    }

    [SerializeField] private AudioClip[] _audioClips;
    public AudioClip[] AudioClips 
    {
        get { return _audioClips; }
    }

    private int _nextClipToPlay;

    private void OnEnable()
    {
        _nextClipToPlay = -1;
    }

    public AudioClip GetNextClip()
    {
        if (AudioClips.Length == 1)
            return AudioClips[0];

        switch (_sequenceMode)
        {
                case PlaybackMode.Sequential:
                    _nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, AudioClips.Length);
                    break;
                case PlaybackMode.Random:
                    _nextClipToPlay = UnityEngine.Random.Range(0, AudioClips.Length);
                    break;
                default:
                    break;
        }

        return AudioClips[_nextClipToPlay];
    }

    public enum PlaybackMode
    {
        Random,
        Sequential
    }
}