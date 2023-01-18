using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    [SerializeField] ScenesData database;
    public GameObject ExitPanel;

    void Start()
    {
        if (database.CurrentMenupage == MenuPage.Options || database.CurrentMenupage == MenuPage.Pause)
            ExitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && database.CurrentMenupage == MenuPage.Options)
        {
            database.UpdateMenuPage(MenuPage.TryToQuit);
        } else if(Input.GetKeyDown(KeyCode.Escape) && database.CurrentMenupage != MenuPage.InGame)
        {
            database.UpdateMenuPage(MenuPage.Options);
        } 
        else if(Input.GetKeyDown(KeyCode.Escape) && database.CurrentMenupage == MenuPage.Pause)
        {
            database.UpdateMenuPage(MenuPage.TryToQuit);
        }
           

        if (Input.GetKeyDown(KeyCode.Y) && database.CurrentMenupage == MenuPage.TryToQuit)
        {
            // Debug.Log("quit game");
            database.UpdateMenuPage(MenuPage.Quit);
        }
        else if (Input.GetKeyDown(KeyCode.N) && database.CurrentMenupage == MenuPage.TryToQuit)
        {
            //  Debug.Log("continue game");
            database.UpdateMenuPage(MenuPage.Options);
        }
    }
}
