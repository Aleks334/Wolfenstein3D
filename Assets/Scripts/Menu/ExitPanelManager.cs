using TMPro;
using UnityEngine;

public class ExitPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject ExitPanel;
    [SerializeField] private ExitQuotesSO _exitQuotesSO;

    [Header("Exit game event channel")]
    [SerializeField] private VoidEventChannelSO _exitEventChannel;

    private bool isVisible = false;

    void Start()
    {
        if (ExitPanel != null)
            ManageExitPanel(isVisible);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isVisible = !isVisible;
            ManageExitPanel(isVisible);
        }

        if(isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _exitEventChannel.RaiseEvent();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                isVisible = false;
                ManageExitPanel(isVisible);
            }
        }
    }

    private void ManageExitPanel(bool visibility)
    {
        ExitPanel.SetActive(visibility);
        if (!visibility)
            return;

        ExitPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
        _exitQuotesSO.exitQuotes[Random.Range(0, _exitQuotesSO.exitQuotes.Length)];
    }
}
