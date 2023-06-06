using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationService 
{
    bool IsPlaying(string name);
    void Play(string name);
}

public interface IFadeService
{
    string FADE_IN { get; set; }
    string FADE_OUT{ get; set; }

    void Fade(string name);
    bool IsCurrentlyFading();
}