using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(Scenes newScene)
    {
        SceneManager.LoadScene((int)newScene);
    }

    //for tests
    public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadUIAsync()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }
}

public enum Scenes
{
    floor_1,
    floor_2,
}
