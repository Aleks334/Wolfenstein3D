using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Configuration")]
public class AudioConfigurationSO : ScriptableObject
{
    [Header("Sound properties")]
    public bool Mute = false;
    [Range(0f, 1f)] public float Volume = 1f;

    public void ApplyTo(AudioSource audioSource)
    {
        audioSource.mute = this.Mute;
        audioSource.volume = this.Volume;
    }
}
