using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneData : ScriptableObject
{
    [Header("Information")]
    public string sceneName;

    [Header("Background music")]
    public AudioClip music;
}
