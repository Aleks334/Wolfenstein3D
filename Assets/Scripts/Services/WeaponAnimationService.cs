using UnityEngine;

public class WeaponAnimationService : AnimationService
{
    private Animator _performer;

    public WeaponAnimationService(Animator performer) : base(performer)
    {
        _performer = performer;
    }

    //For canceling full auto shooting anim when ammo equals 0
    public void CanFullAutoShootAnim(bool canShoot)
    {
        _performer.SetBool("canShoot", canShoot);
    }

    //Plays animation of elevating / lowering full-auto gun
    public void PlayAfterFullAutoShootAnim(PlayerWeapon currentWeapon)
    {
        _performer.Play(currentWeapon.WeaponType.ToString() + "_after_shoot");
    }
}
