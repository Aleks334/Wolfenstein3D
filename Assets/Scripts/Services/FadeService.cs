/// <summary>
/// Service for screeen fading.
/// </summary>

public class FadeService : IFadeService
{
    private IAnimationService _service;

    public FadeService(IAnimationService service)
    {
        _service = service;
    }
    public void Fade(string name)
    {
        if (!_service.IsPlaying(name))
        {
            _service.Play(name);
        }
    }
}