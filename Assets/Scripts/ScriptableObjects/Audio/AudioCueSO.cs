using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio/Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    public bool looping = false;
    [SerializeField] private int _defaultClipGroup = 0;
    public int DefaultClipGroup
    {
        get => _defaultClipGroup;
        set => _defaultClipGroup = value;
    }

    [SerializeField] private AudioClipsGroups[] _audioClipGroups;

    public AudioClip[] GetClips(int clipGroup)
    {
        int numberOfClips = _audioClipGroups[clipGroup].audioClips.Length;
        AudioClip[] resultingClips = new AudioClip[numberOfClips];

        if (_audioClipGroups[clipGroup].sequenceMode == AudioClipsGroups.PlaybackMode.Random)
        {
            resultingClips[0] = _audioClipGroups[clipGroup].GetNextClip();
        }
        else
        {
            for (int i = 0; i < numberOfClips; i++)
            {
                resultingClips[i] = _audioClipGroups[clipGroup].GetNextClip();
            }
        }

        return resultingClips;
    }
}

[Serializable]
public class AudioClipsGroups
{
    public PlaybackMode sequenceMode = PlaybackMode.Sequential;
    public AudioClip[] audioClips;

    private int _nextClipToPlay = -1;

    public AudioClip GetNextClip()
    {
        if (audioClips.Length == 1)
            return audioClips[0];

        if (_nextClipToPlay == -1)
        {
            _nextClipToPlay = (sequenceMode == PlaybackMode.Sequential) ? 0 : UnityEngine.Random.Range(0, audioClips.Length);
        }
        else
        {
            switch (sequenceMode)
            {
                case PlaybackMode.Random:
                    _nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
                    break;

                case PlaybackMode.Sequential:
                    _nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, audioClips.Length);
                    break;
            }
        }

        return audioClips[_nextClipToPlay];
    }

    public enum PlaybackMode
    {
        Sequential,
        Random
    }
}