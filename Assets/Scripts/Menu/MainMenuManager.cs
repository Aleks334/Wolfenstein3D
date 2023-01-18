using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] ScenesData database;
    [SerializeField] MenuControls menuControls;


    private void Start()
    {
        database.OnMenuPageChange += QuitGame_OnMenuPageChange;
       // menuControls = GetComponent<MenuControls>();
    }

    void OnDestroy()
    {
        database.OnMenuPageChange -= QuitGame_OnMenuPageChange;
        
    }

    void QuitGame_OnMenuPageChange(MenuPage menuPage)
    {
        if (database.CurrentMenupage == MenuPage.TryToQuit)
        {
            menuControls.ExitPanel.SetActive(true);
            menuControls.ExitPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = database.exitQuotes[Random.Range(0, database.exitQuotes.Length)];
            
        } else
        {
            menuControls.ExitPanel.SetActive(false);
        }
    }

    /* Methods for menu items START */
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

    //Only for pause menu
    public void BackToGame()
    {
        database.UpdateMenuPage(MenuPage.InGame);
        Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        Camera.main.gameObject.GetComponent<Camera>().enabled = true;
        
    }

    public void TryToQuitGame()
    {
        database.UpdateMenuPage(MenuPage.TryToQuit);
    }

    /* Methods for menu items END */

   
}
