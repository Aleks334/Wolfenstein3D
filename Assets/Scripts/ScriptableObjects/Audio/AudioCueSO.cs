using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio/Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    [Header("Looping works only for single clip!")]
    public bool looping = false;
    [SerializeField] private PlaybackMode sequenceMode = PlaybackMode.Sequential;
    public AudioClip[] audioClips;

    private int _nextClipToPlay = -1;

    private void OnEnable()
    {
        _nextClipToPlay = -1;
    }

    public AudioClip GetNextClip()
    {
        if (audioClips.Length == 1)
            return audioClips[0];

        switch (sequenceMode)
        {
                case PlaybackMode.Sequential:
                    _nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, audioClips.Length);
                    break;
                case PlaybackMode.Random:
                    _nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
                    break;
                default:
                    break;
        }

        return audioClips[_nextClipToPlay];
    }

    public enum PlaybackMode
    {
        Random,
        Sequential
    }
}