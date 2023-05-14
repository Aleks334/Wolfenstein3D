using UnityEngine;
using UnityEngine.AI;

public class Dead : state
{
    [SerializeField] private GameObject _ammoPickUp;

    [SerializeField] private AudioCueSO _deathAudioCue;
    private AudioCue _audioCue;

    private void Start()
    {
        _audioCue = AudioCueComponent;
    }

    public override void on_state_enter()
    {
        _audioCue.AudioData = _deathAudioCue;

        this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        if(this.GetComponent<enemystats>().type == enemystats.enemy_type.Doge)
        {
            //�mier� psa
            PlaySound();
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Hans)
        {
            //�mier� zwyk�ego �o�mierza
            Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
            PlaySound();
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Helmut)
        {
            //�mier� SS
            Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
            PlaySound();
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        }
    }
    public override void state_action()
    {

    }
}
