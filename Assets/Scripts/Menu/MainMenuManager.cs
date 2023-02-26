using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] ScenesData database;
    [SerializeField] MenuControls menuControls;

    [SerializeField] ExitQuotesSO _exitQuotesSO;

    private void Start()
    {
        database.OnMenuPageChange += QuitGame_OnMenuPageChange;
    }

    void OnDisable()
    {
        database.OnMenuPageChange -= QuitGame_OnMenuPageChange;
    }

    void QuitGame_OnMenuPageChange(MenuPage menuPage)
    {
        if (database.CurrentMenupage == MenuPage.TryToQuit)
        {
            menuControls.ExitPanel.SetActive(true);
            menuControls.ExitPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _exitQuotesSO.exitQuotes[Random.Range(0, _exitQuotesSO.exitQuotes.Length)];
            
        } else
        {
            menuControls.ExitPanel.SetActive(false);
        }
    }

    public void NewGame()
    {
        database.UpdateMenuPage(MenuPage.NewGame_EpisodeSelection);
    }

    public void Sound()
    {
        database.UpdateMenuPage(MenuPage.Sound);
    }

    public void Control()
    {
        database.UpdateMenuPage(MenuPage.Control);
    }

    public void LoadGame()
    {
        database.UpdateMenuPage(MenuPage.LoadGame);
    }

    public void GraphicDetails()
    {
        database.UpdateMenuPage(MenuPage.GraphicDetails);
    }

    public void ReadThis()
    {
        database.UpdateMenuPage(MenuPage.ReadThis);
    }
    public void TryToQuitGame()
    {
        database.UpdateMenuPage(MenuPage.TryToQuit);
    }

    //Only for pause menu
    public void BackToGame()
    {
        database.UpdateMenuPage(MenuPage.InGame);
        Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        Camera.main.gameObject.GetComponent<Camera>().enabled = true;
        
    }
}