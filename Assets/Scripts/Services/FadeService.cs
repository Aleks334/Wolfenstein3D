public class FadeService : IFadeService
{
    private IAnimationService _service;
    public string FADE_IN { get; set; } = "FadeIn";
    public string FADE_OUT { get; set; } = "FadeOut";

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

    public bool IsCurrentlyFading()
    {
        if (_service.IsPlaying(FADE_IN) || _service.IsPlaying(FADE_OUT))
            return true;

        return false;
    }
}