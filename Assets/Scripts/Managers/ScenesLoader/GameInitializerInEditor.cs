using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Adding gameobject with this class allows starting game from any scene (it loads initalization scene).
/// </summary>

public class GameInitializerInEditor : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private GameSceneData _initScene;
    [SerializeField] private GameSceneData _ui;
    [SerializeField] private bool _loadUI = true;

    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == _initScene.sceneName)
                return;
        }
        SceneManager.LoadSceneAsync(_initScene.sceneName, LoadSceneMode.Additive);
        if(_loadUI)
            SceneManager.LoadSceneAsync(_ui.sceneName, LoadSceneMode.Additive);
    }
#endif
}