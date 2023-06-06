using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAudioConfiguration : MonoBehaviour
{
    private Slider _slider;
    private TextMeshProUGUI _sliderText;
    private float normalizedNewVal;

    [SerializeField] private GlobaAudioConfigSO _globalAudioConfig;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _sliderText = _slider.GetComponentInChildren<TextMeshProUGUI>();

        _slider.onValueChanged.AddListener((val) =>
        {
            ChangeVolumeGlobally(val);
        });

        _slider.value = _globalAudioConfig.AudioConfigs[0].Volume * 100f;
        ChangeCurrentValueText(_globalAudioConfig.AudioConfigs[0].Volume * 100f);
    }

    public void ChangeVolumeGlobally(float newValue)
    {
        foreach (var config in _globalAudioConfig.AudioConfigs)
        {
            normalizedNewVal = newValue / 100;
            config.Volume = normalizedNewVal;
            config.OnVolumeChanged.RaiseEvent(normalizedNewVal);
        }

        ChangeCurrentValueText(newValue);
    }

    private void ChangeCurrentValueText(float value)
    {
        _sliderText.text = value + "%";
    }
}