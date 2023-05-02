using System;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyLvlSelection : MonoBehaviour
{
    [SerializeField] Button[] difficultyLvlBtns = new Button[4];
    [SerializeField] ScenesData database;

    [SerializeField] AsyncGameLoader gameLoader;

    public Image difficultyLvlImage;
    public Sprite[] DifficultyLvlSprites = new Sprite[4];

    public DifficultyLevel DifficultyLvl { get; private set; }

    public void SelectDifficultyLevel(DifficultyLevel newSelectedDifficultyLvl)
    {
        DifficultyLvl = newSelectedDifficultyLvl;

        //save for database
        database.DifficultyLvl = DifficultyLvl;

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

    void Start()
    {
        SelectDifficultyLevel(DifficultyLevel.None);
       // Debug.Log("Current episode: " + menuData.SelectedEpisode);

        difficultyLvlBtns[0].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)0));
        difficultyLvlBtns[1].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)1));
        difficultyLvlBtns[2].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)2));
        difficultyLvlBtns[3].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)3));
    }

    public void ChooseDifficultyLvl(DifficultyLevel clickedDifficultyLvl)
    {
        SelectDifficultyLevel(clickedDifficultyLvl);
        //Debug.Log("clicked Difficulty Lvl: " + clickedDifficultyLvl);
        gameLoader.LoadGameAsync();
    }
}
