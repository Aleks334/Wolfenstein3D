using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] ScenesData database;
    [SerializeField] Button[] episodeBtns = new Button[6];

    public Episodes SelectedEpisode { get; private set; }

    public void SelectEpisode(Episodes newSelectedEpisode)
    {
        SelectedEpisode = newSelectedEpisode;

        //save for database
        database.SelectedEpisode = SelectedEpisode;

        switch (SelectedEpisode)
        {
            case Episodes.None:
                Debug.Log("Default episode: " + SelectedEpisode);
                break;
            case Episodes.Episode_1:
                Debug.Log("Selected Episode: " + SelectedEpisode);
                break;
            case Episodes.Episode_2:
                Debug.Log("Selected Episode: " + SelectedEpisode);
                break;
            case Episodes.Episode_3:
                Debug.Log("Selected Episode: " + SelectedEpisode);
                break;
            case Episodes.Episode_4:
                Debug.Log("Selected Episode: " + SelectedEpisode);
                break;
            case Episodes.Episode_5:
                Debug.Log("Selected Episode: " + SelectedEpisode);
                break;
            case Episodes.Episode_6:
                Debug.Log("Selected Episode: " + SelectedEpisode);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(SelectedEpisode), SelectedEpisode, null);
        }

    }

    private void Start()
    {
        SelectEpisode(Episodes.None);

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
        SelectEpisode(clickedEpisode);
        database.UpdateMenuPage(MenuPage.NewGame_DifficultyLevelSelection);
    }
}
