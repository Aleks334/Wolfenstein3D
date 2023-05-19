using UnityEngine;

public class MenuControls : MonoBehaviour
{
    [Header("Load Scene fields")]
    [SerializeField] private LoadSceneEventChannelSO _loadMenuTabEventChannel;
    [SerializeField] private MenuTab _currentMenuTab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _currentMenuTab.PreviousMenuTab != null)
        {
            _loadMenuTabEventChannel.RaiseEvent(new[] { _currentMenuTab.PreviousMenuTab }, false);
        }
    }
}