using UnityEngine;

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
        AudioCueComponent.PlayAudioCue();
    }

    private void ChangeDefaultClipGroup(int clipGroupIndex)
    {
        AudioCueComponent.AudioData.DefaultClipGroup = clipGroupIndex;
    }
}