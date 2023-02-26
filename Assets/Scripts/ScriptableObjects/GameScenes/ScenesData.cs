using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewSceneDB", menuName = "Game_Data/Database")]
public class ScenesData : ScriptableObject
{
    /*--------------------------
     * Episode Selection START
     * -------------------------*/
    public Episode[] episodes = new Episode[6];

    private Episodes _selectedEpisode;

    public Episodes SelectedEpisode
    {
        get { return _selectedEpisode; }

        private set { _selectedEpisode = value; }
    }

    public void SelectEpisode(Episodes newSelectedEpisode)
    {
        SelectedEpisode = newSelectedEpisode;

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
    /* -----------------------
     * Episode Selection END
     * ------------------------*/


    /*------------------
     * Menu page START
     ------------------*/
    public List<MenuTab> menuTabs = new List<MenuTab>();
    
    public event Action<MenuPage> OnMenuPageChange;
    [SerializeField] private MenuPage _currentMenuPage;

    public MenuPage CurrentMenupage
    {
        get { return _currentMenuPage; }

        private set { _currentMenuPage = value; }
    }

    public void UpdateMenuPage(MenuPage menuPageToLoad)
    {
        CurrentMenupage = menuPageToLoad;

        switch (menuPageToLoad)
        {
            case MenuPage.None:
                Debug.Log("current state: " + CurrentMenupage);
                break;
            case MenuPage.SplashScreen:
                Debug.Log("current state: " + CurrentMenupage);
                break;
            case MenuPage.Options:
                Debug.Log("current state: " + CurrentMenupage);
                LoadScene(menuTabs[(int)MenuPage.Options].sceneName);
                break;
            case MenuPage.NewGame_EpisodeSelection:
                Debug.Log("current state: " + CurrentMenupage);
                LoadScene(menuTabs[(int)MenuPage.NewGame_EpisodeSelection].sceneName);
                break;
            case MenuPage.NewGame_DifficultyLevelSelection:
                Debug.Log("current state: " + CurrentMenupage);
                LoadScene(menuTabs[(int)MenuPage.NewGame_DifficultyLevelSelection].sceneName);
                break;
            case MenuPage.InGame:
                Debug.Log("current state: " + CurrentMenupage);
                LoadSceneAdditive("UI");
                /*if (SceneManager.GetSceneByName(menuTabs[(int)MenuPage.Pause].sceneName).isLoaded)
                {
                    UnloadSceneAsync(menuTabs[(int)MenuPage.Pause].sceneName);
                }*/
                break;
            case MenuPage.Sound:
                Debug.Log("current state: " + CurrentMenupage);
                //load scene
                break;
            case MenuPage.Control:
                Debug.Log("current state: " + CurrentMenupage);
                LoadScene(menuTabs[(int)MenuPage.Control].sceneName);
                break;
            case MenuPage.LoadGame:
                Debug.Log("current state: " + CurrentMenupage);
                //load scene
                break;
            case MenuPage.GraphicDetails:
                Debug.Log("current state: " + CurrentMenupage);
                //load scene
                break;
            case MenuPage.ReadThis:
                Debug.Log("current state: " + CurrentMenupage);
                LoadScene(menuTabs[(int)MenuPage.ReadThis].sceneName);
                break;
            case MenuPage.Pause:
                Debug.Log("current state: " + CurrentMenupage);
                LoadSceneAdditive(menuTabs[(int)MenuPage.Pause].sceneName);
                UnloadSceneAsync("UI");
                break;
            case MenuPage.TryToQuit:
                Debug.Log("current state: " + CurrentMenupage);
                OnMenuPageChange?.Invoke(menuPageToLoad);
                break;
            case MenuPage.Quit:
                Debug.Log("current state: " + CurrentMenupage);
                Debug.Log("Player left the game (doesn't work in editor)");
                Application.Quit();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(menuPageToLoad), menuPageToLoad, null);
        }
    }

    void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void LoadSceneAdditive(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    void UnloadSceneAsync(string sceneToLoad)
    {
        SceneManager.UnloadSceneAsync(sceneToLoad);
    }
    /*----------------
     * Menu page END
     * ---------------*/


    /*--------------------------
    * Difficulty Level Selection START
    * -------------------------*/
    private DifficultyLevel _difficultyLvl;

    public DifficultyLevel DifficultyLvl
    {
        get { return _difficultyLvl; }

        private set { _difficultyLvl = value; }
    }

    public void SelectDifficultyLevel(DifficultyLevel newSelectedDifficultyLvl)
    {
        DifficultyLvl = newSelectedDifficultyLvl;

        switch (DifficultyLvl)
        {
            case DifficultyLevel.None:
                Debug.Log("Default difficulty level: " + DifficultyLvl);
                break;
            case DifficultyLevel.Baby:
                Debug.Log("Selected difficulty level: " + DifficultyLvl);
                break;
            case DifficultyLevel.Easy:
                Debug.Log("Selected difficulty level: " + DifficultyLvl);
                break;
            case DifficultyLevel.Medium:
                Debug.Log("Selected difficulty level: " + DifficultyLvl);
                break;
            case DifficultyLevel.Hard:
                Debug.Log("Selected difficulty level: " + DifficultyLvl);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(DifficultyLvl), DifficultyLvl, null);
        }
    }
    /* -----------------------
     * Difficulty Level Selection END
     * ------------------------*/
}


public enum MenuPage
{
    None = -1,
    SplashScreen,
    Options,
    NewGame_EpisodeSelection,
    NewGame_DifficultyLevelSelection,
    Sound,
    Control,
    LoadGame,
    GraphicDetails,
    ReadThis,
    Pause,
    //The order of placement of the following MenuPage states doesn't matter
    TryToQuit,
    Quit,
    InGame,
}
public enum Episodes
{
    None = -1,
    Episode_1,
    Episode_2,
    Episode_3,
    Episode_4,
    Episode_5,
    Episode_6,
}

public enum DifficultyLevel
{
    None = -1,
    Baby,
    Easy,
    Medium,
    Hard,
}