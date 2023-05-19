using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] ScenesData database;

    public void ChooseEpisode(Episode newSelectedEpisode)
    {
        database.SelectedEpisode = newSelectedEpisode;
        Debug.LogWarning("Selected episode set to: " + database.SelectedEpisode.sceneName);
    }
}