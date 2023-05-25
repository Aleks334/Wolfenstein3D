public interface IAnimationService 
{
    bool IsPlaying(string name);
    void Play(string name);
}

public interface IFadeService
{
    void Fade(string name);
}