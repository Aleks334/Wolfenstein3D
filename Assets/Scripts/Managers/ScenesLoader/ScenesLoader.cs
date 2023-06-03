using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    private List<GameSceneData> _scenesToLoad = new();
    private List<AsyncOperation> _scenesToLoadAsync = new();
    private List<string> _scenesToUnload = new();

    private GameSceneData _activeScene;
    private bool _isBusy = false;

    [Header("Fading screen")]
    [SerializeField] private Animator _fadingScreen;

    private IFadeService _fadeService;

    private void Awake()
    {
        _loadingInterface.SetActive(false);

        _fadeService = new FadeService(new AnimationService(_fadingScreen));
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
        if (_isBusy)
            return;

        _isBusy = true;
        _scenesToLoad = scenesToLoad.ToList();

        AddScenesToUnload();
       
        if (showProgressBar)
            StartCoroutine(ShowLoadingSceneProgress());      
        else
            StartCoroutine(ShowFadingOnSceneLoading());

    }

    private void AddScenesToUnload()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == _initScene.sceneName)
                continue;

            _scenesToUnload.Add(SceneManager.GetSceneAt(i).name);
        }
    }

    private void UnloadOtherScenes()
    {
        for (int i = 0; i < _scenesToUnload.Count; i++)
        {
            SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
        }

        _scenesToUnload.Clear();
    }

    private void PerformAsyncLoading()
    {
        for (int i = 0; i < _scenesToLoad.Count; i++)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_scenesToLoad[i].sceneName, LoadSceneMode.Additive);
            _scenesToLoadAsync.Add(op);
        }
        _activeScene = _scenesToLoad[0];

        _scenesToLoadAsync[0].completed += SetNewActiveScene;
    }

    private void SetNewActiveScene(AsyncOperation asyncOperation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activeScene.sceneName));
    }

    
    private IEnumerator ShowLoadingSceneProgress()
    {
        _loadingInterface.SetActive(true);
        float totalProgress = 0f;

        UnloadOtherScenes();
        PerformAsyncLoading();

        for (int i = 0; i < _scenesToLoadAsync.Count; ++i)
        { 
            while (_scenesToLoadAsync[i].progress < 0.9f)
            {
                totalProgress += _scenesToLoadAsync[i].progress;
                _loadingProgressBar.fillAmount = totalProgress / _scenesToLoadAsync.Count;
                yield return null;
            }
        }
        
        _loadingInterface.SetActive(false);
        _scenesToLoadAsync.Clear();

        _isBusy = false;
    }

    private IEnumerator ShowFadingOnSceneLoading()
    {
        _fadeService.Fade(_fadeService.FADE_IN);
        yield return null;

        while (_fadeService.IsCurrentlyFading())
        {
            yield return null;
        }

        UnloadOtherScenes();
        _fadeService.Fade(_fadeService.FADE_OUT);

        PerformAsyncLoading();
        _scenesToLoadAsync.Clear();
        
        _isBusy = false;
    }
}