using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Load Scene Event Channel")]
public class LoadSceneEventChannelSO : ScriptableObject
{
    public event Action<GameSceneData, bool, bool> OnSceneLoadingRequested;

    public void RaiseEvent(GameSceneData sceneToLoad, bool showProgressBar, bool unloadOtherScenes)
    {
        if (OnSceneLoadingRequested != null)
        {
            OnSceneLoadingRequested?.Invoke(sceneToLoad, showProgressBar, unloadOtherScenes);
        }
        else
            Debug.LogError("Nobody subscribed to event of type " + this.name);
    }
}