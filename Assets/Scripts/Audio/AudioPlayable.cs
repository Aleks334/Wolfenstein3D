using UnityEngine;

//[RequireComponent(typeof(AudioCue))]
public class AudioPlayable : MonoBehaviour
{
    protected void PlaySound()
    {
        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.PlayAudioCue();
        else
            Debug.LogError("Object doesn't have AudioCue");
    }

    protected void ChangeDefaultClipGroup(int clipGroupIndex)
    {
        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.AudioData.DefaultClipGroup = clipGroupIndex;
        else
            Debug.LogError("Object doesn't have AudioCue");
    }
}