using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreensManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _splashPanels = new();
    [SerializeField] private Animator _fadingScreen;
    [SerializeField] private int _currentPanel;

    [Header("Load Scene fields")]
    [SerializeField] private LoadSceneEventChannelSO _loadSceneEventChannel;
    [SerializeField] private MenuTab _menuTabToLoad;

    private IFadeService _fadeService;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for(int i = 0; i < _splashPanels.Count; i++)
        {
            if(i != 0)
                SetSplashVisibility(_splashPanels[i], false);
        }

        _fadeService = new FadeService(new AnimationService(_fadingScreen));
    }

    void Update()
    {
        if (Input.anyKeyDown && !_fadeService.IsCurrentlyFading())
        {
            StartCoroutine(DoFade());
        }
    }

    private IEnumerator DoFade()
    {
        if (_currentPanel < _splashPanels.Count - 1)
        {
            _fadeService.Fade(_fadeService.FADE_IN);
            yield return null;

            while (_fadeService.IsCurrentlyFading())
            {
                yield return null;
            }

            _fadeService.Fade(_fadeService.FADE_OUT);

            SetSplashVisibility(_splashPanels[_currentPanel], false);
            SetSplashVisibility(_splashPanels[++_currentPanel], true);
        }
        else
        {
            _fadeService.Fade(_fadeService.FADE_IN);   

            _loadSceneEventChannel.RaiseEvent(new[] { _menuTabToLoad }, false);
        }
    }

    private void SetSplashVisibility(GameObject splash, bool visible)
    {
        splash.gameObject.SetActive(visible);
    }
}