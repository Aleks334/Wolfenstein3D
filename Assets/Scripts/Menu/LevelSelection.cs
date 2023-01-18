using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] ScenesData database;
    [SerializeField] Button[] episodeBtns = new Button[6]; 

    private void Start()
    {
        database.SelectEpisode(Episodes.None);

        /* this adds listener for every episode btn. TODO: replace it with loop */
      
        episodeBtns[0].onClick.AddListener(() => ChooseEpisode((Episodes)0));
        episodeBtns[1].onClick.AddListener(() => ChooseEpisode((Episodes)1));
        episodeBtns[2].onClick.AddListener(() => ChooseEpisode((Episodes)2));
        episodeBtns[3].onClick.AddListener(() => ChooseEpisode((Episodes)3));
        episodeBtns[4].onClick.AddListener(() => ChooseEpisode((Episodes)4));
        episodeBtns[5].onClick.AddListener(() => ChooseEpisode((Episodes)5));

    }

    public void ChooseEpisode(Episodes clickedEpisode)
    {
        database.SelectEpisode(clickedEpisode); // TODO: Load different scene depending on selected episode (clicked btn)
                                                // Debug.Log("clickedEpisode: " + clickedEpisode);
        database.UpdateMenuPage(MenuPage.NewGame_DifficultyLevelSelection);
    }
}
