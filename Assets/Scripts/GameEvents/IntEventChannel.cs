using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    public event Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
        {
           // Debug.LogWarning("Raised event" + this.name);
            OnEventRaised.Invoke(value);
        }
            
        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
}