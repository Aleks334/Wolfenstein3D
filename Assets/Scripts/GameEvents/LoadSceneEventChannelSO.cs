using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Load Scene Event Channel")]
public class LoadSceneEventChannelSO : ScriptableObject
{
    public event Action<GameSceneData[], bool> OnSceneLoadingRequested;

    public void RaiseEvent(GameSceneData[] scenesToLoad, bool showProgressBar)
    {
        if (OnSceneLoadingRequested != null)
        {
            OnSceneLoadingRequested?.Invoke(scenesToLoad, showProgressBar);
        }
        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
}