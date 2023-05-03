using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneDB", menuName = "Game_Data/Database")]
public class ScenesData : ScriptableObject
{
    public Episode[] episodes = new Episode[6];
    public List<MenuTab> menuTabs = new List<MenuTab>();

    public event Action<MenuPage> OnMenuPageChange;

    public Episodes SelectedEpisode { get; set; }
    public DifficultyLevel DifficultyLvl { get; set; }
    public MenuPage CurrentMenupage { get; private set; }

    private ScenesFlowController _scenesFlowController;
    private ScenesFlowController ScenesController
    {
        get
        {
            if (_scenesFlowController != null)
                return _scenesFlowController;
            else
            {
                _scenesFlowController = new ScenesFlowController();
                return _scenesFlowController;
            }
               
        }
        set { _scenesFlowController = value;  }
    }

    public void UpdateMenuPage(MenuPage menuPageToLoad)
    {
        CurrentMenupage = menuPageToLoad;

        switch (menuPageToLoad)
        {
            case MenuPage.None:
                Debug.LogError("None page state");
                break;
            case MenuPage.SplashScreen:
                //Debug.Log("current state: " + CurrentMenupage);
                break;
            case MenuPage.Options:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadScene(menuTabs[(int)MenuPage.Options].sceneName);
                break;
            case MenuPage.NewGame_EpisodeSelection:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadScene(menuTabs[(int)MenuPage.NewGame_EpisodeSelection].sceneName);
                break;
            case MenuPage.NewGame_DifficultyLevelSelection:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadScene(menuTabs[(int)MenuPage.NewGame_DifficultyLevelSelection].sceneName);
                break;
            case MenuPage.InGame:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadSceneAdditive("UI");
                break;
            case MenuPage.Sound:
                //Debug.Log("current state: " + CurrentMenupage);
                //load scene
                break;
            case MenuPage.Control:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadScene(menuTabs[(int)MenuPage.Control].sceneName);
                break;
            case MenuPage.LoadGame:
                //Debug.Log("current state: " + CurrentMenupage);
                //load scene
                break;
            case MenuPage.GraphicDetails:
                //Debug.Log("current state: " + CurrentMenupage);
                //load scene
                break;
            case MenuPage.ReadThis:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadScene(menuTabs[(int)MenuPage.ReadThis].sceneName);
                break;
            case MenuPage.Pause:
                //Debug.Log("current state: " + CurrentMenupage);
                ScenesController.LoadSceneAdditive(menuTabs[(int)MenuPage.Pause].sceneName);
                ScenesController.UnloadSceneAsync("UI");
                break;
            case MenuPage.TryToQuit:
                //Debug.Log("current state: " + CurrentMenupage);
                OnMenuPageChange?.Invoke(menuPageToLoad);
                break;
            case MenuPage.Quit:
                //Debug.Log("current state: " + CurrentMenupage);
                //Debug.Log("Player left the game (doesn't work in editor)");
                Application.Quit();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(menuPageToLoad), menuPageToLoad, null);
        }
    }
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