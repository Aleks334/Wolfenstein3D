using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyLvlSelection : MonoBehaviour
{
    [SerializeField] Button[] difficultyLvlBtns = new Button[4];
    [SerializeField] ScenesData database;

    [SerializeField] AsyncGameLoader gameLoader;

    public Image difficultyLvlImage;
    public Sprite[] DifficultyLvlSprites = new Sprite[4];

    void Start()
    {
        database.SelectDifficultyLevel(DifficultyLevel.None);
       // Debug.Log("Current episode: " + menuData.SelectedEpisode);

        difficultyLvlBtns[0].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)0));
        difficultyLvlBtns[1].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)1));
        difficultyLvlBtns[2].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)2));
        difficultyLvlBtns[3].onClick.AddListener(() => ChooseDifficultyLvl((DifficultyLevel)3));
    }

    public void ChooseDifficultyLvl(DifficultyLevel clickedDifficultyLvl)
    {
        database.SelectDifficultyLevel(clickedDifficultyLvl); // TODO: Load different scene depending on selected episode (clicked btn)
        //Debug.Log("clicked Difficulty Lvl: " + clickedDifficultyLvl);
        gameLoader.LoadGameAsync();
    }
}
