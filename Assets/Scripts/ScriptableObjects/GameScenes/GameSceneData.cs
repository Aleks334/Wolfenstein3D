using UnityEngine;

public class GameSceneData : ScriptableObject
{
    [Header("Information")]
    public string sceneName;

    [Header("Background music")]
    [SerializeField] private AudioCueSO _backgroundMusic;

    public AudioCueSO BackgroundMusic
    {
        get => _backgroundMusic;
        set => _backgroundMusic = value;
    }
}