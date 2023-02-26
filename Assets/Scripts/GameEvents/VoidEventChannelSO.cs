using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        if(OnEventRaised != null)
            OnEventRaised.Invoke();
        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
}