using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio/Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    public bool looping = false;
    [SerializeField] private AudioClipsGroup _audioClipGroup;

    public AudioClip[] GetClips()
    {
        int numberOfClips = _audioClipGroup.audioClips.Length;
        AudioClip[] resultingClips = new AudioClip[numberOfClips];

        if (_audioClipGroup.sequenceMode == AudioClipsGroup.PlaybackMode.Random)
        {
            resultingClips[0] = _audioClipGroup.GetNextClip();
        }
        else
        {
            for (int i = 0; i < numberOfClips; i++)
            {
                resultingClips[i] = _audioClipGroup.GetNextClip();
            }
        }

        return resultingClips;
    }
}

[Serializable]
public class AudioClipsGroup
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
        Random,
        Sequential,
    }
}