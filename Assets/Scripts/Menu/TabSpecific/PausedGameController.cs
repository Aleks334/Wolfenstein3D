using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedGameController : MonoBehaviour
{
    [SerializeField] ScenesData database;

    void Update()
    {
       /* if(Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
            Camera.main.gameObject.GetComponent<Camera>().enabled = false;
            
            database.UpdateMenuPage(MenuPage.Pause);
            // Time.timeScale = 0;
            
        }*/
    }
}
