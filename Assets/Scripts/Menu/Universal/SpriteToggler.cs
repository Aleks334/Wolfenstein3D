using System;
using UnityEngine;
using UnityEngine.UI;

public class SpriteToggler : MonoBehaviour
{
    private bool _enabled;

    [SerializeField] private Sprite _toggleOn;
    [SerializeField] private Sprite _toggleOff;

    public event Action<SpriteToggler> ToggledBtn;

    public void SwitchOn(Image renderer)
    {
        if (_enabled)
            return;

        _enabled = true;

        if (_enabled)
            renderer.sprite = _toggleOn;
        else
            renderer.sprite = _toggleOff;

        ToggledBtn.Invoke(this);
    }

    public void SwitchOff(Image renderer)
    {
        _enabled = false;
        renderer.sprite = _toggleOff;
    }
}