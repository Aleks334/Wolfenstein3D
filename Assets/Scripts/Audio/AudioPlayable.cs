using UnityEngine;

//[RequireComponent(typeof(AudioCue))]
public class AudioPlayable : MonoBehaviour
{
    public AudioCue AudioCueComponent
    {
        get
        {
            if (TryGetComponent<AudioCue>(out AudioCue audioCue))
                return audioCue;
            else
            {
                Debug.LogError($"{gameObject.name} doesn't have AudioCue component attached");
                return null;
            }      
        }
    }

    protected void PlaySound(int clipGroupIndex = 0)
    {
        ChangeDefaultClipGroup(clipGroupIndex);

        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.PlayAudioCue();
        else
            Debug.LogError("Object doesn't have AudioCue");
    }

    private void ChangeDefaultClipGroup(int clipGroupIndex)
    {
        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.AudioData.DefaultClipGroup = clipGroupIndex;
        else
            Debug.LogError("Object doesn't have AudioCue");
    }
}