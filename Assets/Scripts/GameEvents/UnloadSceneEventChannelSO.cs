using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Unload Scene Event Channel")]
public class UnloadSceneEventChannelSO : ScriptableObject
{
    /*
    public event Action<GameSceneData[]> OnSceneUnloadingRequested;

    public void RaiseEvent(GameSceneData[] scenesToUnload)
    {
        if (OnSceneUnloadingRequested != null)
        {
            OnSceneUnloadingRequested?.Invoke(scenesToUnload);
        }
        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
    */
}