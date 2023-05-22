using TMPro;
using UnityEngine;

public class ExitPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject ExitPanel;
    [SerializeField] private ExitQuotesSO _exitQuotesSO;

    [Header("Exit game event channel")]
    [SerializeField] private VoidEventChannelSO _exitEventChannel;

    private bool isVisible = false;
    private AudioCue _audioCue;

    void Start()
    {
        if (ExitPanel != null)
            ManageExitPanel(isVisible);

        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            _audioCue = audioCue;
        else
            Debug.LogError("Background music player doesn't have an AudioCue!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _audioCue.PlayAudioCue();

            isVisible = !isVisible;
            ManageExitPanel(isVisible);
        }

        if(isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _audioCue.PlayAudioCue();
                _exitEventChannel.RaiseEvent();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                _audioCue.PlayAudioCue();
                isVisible = false;
                ManageExitPanel(isVisible);
            }
        }
    }

    public void ManageExitPanel(bool visibility)
    {
        isVisible = visibility;

        ExitPanel.SetActive(isVisible);
        if (!isVisible)
            return;

        ExitPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
        _exitQuotesSO.exitQuotes[Random.Range(0, _exitQuotesSO.exitQuotes.Length)];
    }
}
