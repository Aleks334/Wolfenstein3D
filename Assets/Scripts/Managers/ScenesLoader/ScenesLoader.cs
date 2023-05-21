using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ScenesLoader : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField] private LoadSceneEventChannelSO _loadSceneEventChannel;

    [Header("Scenes")]
    [SerializeField] private GameSceneData _initScene;
    [SerializeField] private MenuTab[] _scenesOnFirstLoad;

    [Header("Progress bar")]
    [SerializeField] private GameObject _loadingInterface;
    [SerializeField] private Image _loadingProgressBar;

    private List<AsyncOperation> _scenesToLoadAsync = new();

    private GameSceneData _activeScene;

    [SerializeField] private ScenesData _database;

    private void Awake()
    {
        _loadingInterface.SetActive(false);
        _database.SubscribeToLoadingSceneEvent();
    } 

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == _initScene.sceneName)
            _loadSceneEventChannel.RaiseEvent(_scenesOnFirstLoad, false);
    }

    private void OnEnable()
    {
        _loadSceneEventChannel.OnSceneLoadingRequested += LoadSceneAsync;
    }

    private void OnDisable()
    {
        _loadSceneEventChannel.OnSceneLoadingRequested -= LoadSceneAsync;
    }


    private void LoadSceneAsync(GameSceneData[] scenesToLoad, bool showProgressBar)
    { 
        UnloadOtherScenes();
        _activeScene = scenesToLoad[0];

        for (int i = 0; i < scenesToLoad.Length; i++)
        {
           // if (!IsSceneLoaded(scenesToLoad[i]))
          //  {
                _scenesToLoadAsync.Add(SceneManager.LoadSceneAsync(scenesToLoad[i].sceneName, LoadSceneMode.Additive));
                //Debug.LogWarning($"Loaded scene: {scenesToLoad[i].sceneName}");
          //  }
        }

        _scenesToLoadAsync[0].completed += SetNewActiveScene; 

        if (showProgressBar)
        {
            StartCoroutine(ShowLoadingSceneProgress());
        }   
        else
            _scenesToLoadAsync.Clear();
    }

    private void SetNewActiveScene(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activeScene.sceneName));
    }

    private void UnloadOtherScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == _initScene.sceneName)
                continue;

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            //Debug.LogWarning($"Unloaded scene: {SceneManager.GetSceneAt(i).name}");
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

        for (int i = 0; i < _scenesToLoadAsync.Count; ++i)
        { 
            while (!_scenesToLoadAsync[i].isDone)
            {
                totalProgress += _scenesToLoadAsync[i].progress;
                //Debug.Log("progress: " + totalProgress);
                _loadingProgressBar.fillAmount = totalProgress / _scenesToLoadAsync.Count;
               // Debug.Log("Fill: " + _loadingProgressBar.fillAmount);
                yield return null;
            }
        }
        _scenesToLoadAsync.Clear();
        _loadingInterface.SetActive(false);
    }
}