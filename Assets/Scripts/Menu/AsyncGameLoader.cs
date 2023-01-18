using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncGameLoader : MonoBehaviour
{
    [SerializeField] ScenesData database;

    [SerializeField] GameObject mainMenuObj;
    [SerializeField] GameObject loadingInterfaceObj;
    [SerializeField] Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    void Start()
    {
        loadingInterfaceObj.SetActive(false);
    }

    public void LoadGameAsync()
    {
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync(database.episodes[(int)database.SelectedEpisode].sceneName));
        StartCoroutine(LoadingScreen());
    }

    void HideMenu()
    {
        mainMenuObj.SetActive(false);
    }

    void ShowLoadingScreen()
    {
        loadingInterfaceObj.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
              //  Debug.Log("progress: " + totalProgress);
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
              //  Debug.Log("Fill: " + loadingProgressBar.fillAmount);
                yield return null;
            }
        }
    }
}
