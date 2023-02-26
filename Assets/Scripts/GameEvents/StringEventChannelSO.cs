using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannelSO : ScriptableObject
{
    public event Action<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        if (OnEventRaised != null)
        {
            //Debug.LogWarning("Raised event" + this.name);
            OnEventRaised.Invoke(value);
        }

        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
}