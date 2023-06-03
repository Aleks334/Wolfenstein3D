using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Configuration")]
public class AudioConfigurationSO : ScriptableObject
{
    [Header("Sound properties (add this SO to global config list!)")]
    public bool Mute = false;
    [Range(0f, 1f)] public float Volume = 1f;
    [Range(-3f, 3f)] public float Pitch = 1f;
    [Range(-1f, 1f)] public float PanStereo = 0f;

    [Header("Spatialisation (3D Sounds)")]

    [Tooltip("Set to 1 in order to have 3D Sound")]
    [Range(0f, 1f)] public float SpatialBlend = 0f;

    [Tooltip("Set to Linear in order to have 3D Sound")]
    public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;

    [Range(0.1f, 20f)] public float MinDistance = 0.1f;
    [Range(10f, 200f)] public float MaxDistance = 60f;

    [Header("Event Channels")]
    public FloatEventChannel OnVolumeChanged;

    public void ApplyTo(AudioSource audioSource)
    {
        audioSource.mute = this.Mute;
        audioSource.volume = this.Volume;
        audioSource.pitch = this.Pitch;
        audioSource.panStereo = this.PanStereo;
        audioSource.spatialBlend = this.SpatialBlend;
        audioSource.rolloffMode = this.RolloffMode;
        audioSource.minDistance = this.MinDistance;
        audioSource.maxDistance = this.MaxDistance;
    }
}
