using UnityEngine;

public class LoadSceneOnKeyPress : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadSceneEventChannel;
    [SerializeField] private GameSceneData _sceneToLoad;

    private void Update()
    {
        if (Input.anyKeyDown)
            _loadSceneEventChannel.RaiseEvent(new[] { _sceneToLoad }, false);
    }
}