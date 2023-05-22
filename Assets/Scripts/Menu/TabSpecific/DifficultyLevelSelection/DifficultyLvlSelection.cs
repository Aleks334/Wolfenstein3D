using UnityEngine;
using UnityEngine.UI;

public class DifficultyLvlSelection : MonoBehaviour
{
    [SerializeField] ScenesData database;

    public Image difficultyLvlImage;
    public Sprite[] DifficultyLvlSprites = new Sprite[4];


    public void ChooseDifficultyLevel(DifficultyLevelSO newSelectedDifficultyLvl)
    {
        database.DifficultyLvl = newSelectedDifficultyLvl;
        Debug.LogWarning("Selected Difficulty level: " + database.DifficultyLvl.Name);
    }
}