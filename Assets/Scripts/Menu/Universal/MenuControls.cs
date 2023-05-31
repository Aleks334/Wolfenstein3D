using UnityEngine;

public class MenuControls : MonoBehaviour
{
    [Header("Load Scene fields")]
    [SerializeField] private LoadSceneEventChannelSO _loadMenuTabEventChannel;
    [SerializeField] private MenuTab _currentMenuTab;

    private AudioCue _audioCue;

    private void Start()
    {
        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            _audioCue = audioCue;
        else
            Debug.LogError("Background music player doesn't have an AudioCue!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _currentMenuTab.PreviousMenuTab != null)
        {
            _audioCue.PlayAudioCue();

            _loadMenuTabEventChannel.RaiseEvent(new[] { _currentMenuTab.PreviousMenuTab }, false);
        }
    }
}