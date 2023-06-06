using UnityEngine;

public class AnimationService : IAnimationService
{
    private Animator _performer;

   public AnimationService(Animator performer)
   {
        _performer = performer;
   }
    public bool IsPlaying(string name)
    {
        if (_performer.GetCurrentAnimatorStateInfo(0).IsName(name) &&
           _performer.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return true;
        else
            return false;
    }

    public void Play(string name)
    {
        _performer.Play(name);
    }
}
