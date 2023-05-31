using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private LoadSceneEventChannelSO _loadMenuTabEventChannel;

    public void LoadScene(MenuTab requestedScene)
    {
        _loadMenuTabEventChannel.RaiseEvent(new[] { requestedScene }, false);
    }
}