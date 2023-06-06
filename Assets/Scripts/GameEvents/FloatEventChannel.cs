using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannel : ScriptableObject
{
    public event Action<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }

        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
}