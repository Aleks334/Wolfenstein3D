using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesLoader : MonoBehaviour
{
    [Header("Event Channel")]
    [SerializeField] private LoadSceneEventChannelSO _loadSceneEventChannel;

    [Header("Scenes")]
    [SerializeField] private GameSceneData _initScene;
    [SerializeField] private MenuTab _splashScreen;

    [Header("Progress bar")]
    [SerializeField] private GameObject _loadingInterface;
    [SerializeField] private Image _loadingProgressBar;

    private AsyncOperation _sceneToLoadAsync;
    private GameSceneData _activeScene;

    private void Awake() => _loadingInterface.SetActive(false);

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == _initScene.sceneName)
            LoadSceneAsync(_splashScreen, false, true);
    }

    private void OnEnable() => _loadSceneEventChannel.OnSceneLoadingRequested += LoadSceneAsync;

    private void OnDisable() => _loadSceneEventChannel.OnSceneLoadingRequested -= LoadSceneAsync;

    private void LoadSceneAsync(GameSceneData sceneToLoad, bool showProgressBar, bool unloadOtherScenes)
    {
        if (unloadOtherScenes)
            UnloadOtherScenes();

        _activeScene = sceneToLoad;

        if (!IsSceneLoaded(sceneToLoad))
            _sceneToLoadAsync = SceneManager.LoadSceneAsync(sceneToLoad.sceneName, LoadSceneMode.Additive);

        //New active scene is needed for proper work of lightning and skybox.
        _sceneToLoadAsync.completed += SetNewActiveScene;

        if (showProgressBar)
            StartCoroutine(ShowLoadingSceneProgress());
    }

    private void SetNewActiveScene(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activeScene.sceneName));
        _activeScene = default;
    }

    private void UnloadOtherScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == _initScene.sceneName)
                continue;

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            Debug.Log($"Unloaded scene: {SceneManager.GetSceneAt(i).name}");
        }
    }

    private bool IsSceneLoaded(GameSceneData checkedScene)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == checkedScene.sceneName)
                return true;
        }

        return false;
    }

    private IEnumerator ShowLoadingSceneProgress()
    {
        _loadingInterface.SetActive(true);

        float totalProgress = 0f;
        while (!_sceneToLoadAsync.isDone)
        {
            totalProgress += _sceneToLoadAsync.progress;
            //   _loadingProgressBar.fillAmount = totalProgress / ;
            Debug.Log($"progress: {totalProgress}. Fill: {_loadingProgressBar.fillAmount}");
            yield return null;
        }

        _sceneToLoadAsync = default;
        _loadingInterface.SetActive(false);
    }
}