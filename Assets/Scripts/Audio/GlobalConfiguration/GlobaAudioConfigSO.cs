using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Global Audio Configuration")]
public class GlobaAudioConfigSO : ScriptableObject
{
    [SerializeField] private List<AudioConfigurationSO> _audioConfigs;

    public List<AudioConfigurationSO> AudioConfigs
    {
        get => _audioConfigs;
    }
}