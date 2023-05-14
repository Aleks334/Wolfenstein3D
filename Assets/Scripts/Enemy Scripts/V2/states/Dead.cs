using UnityEngine;
using UnityEngine.AI;

public class Dead : state
{
    [SerializeField] private GameObject _ammoPickUp;
    [SerializeField] private GameObject _deadEnemy;

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
            //œmieræ psa
            PlaySound();
            Instantiate(_deadEnemy, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Hans)
        {
            //œmieræ zwyk³ego ¿o³mierza
            PlaySound();
            Instantiate(_deadEnemy, transform.position, Quaternion.identity);
            Instantiate(_ammoPickUp, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Helmut)
        {
            //œmieræ SS
            PlaySound();
            Instantiate(_deadEnemy, transform.position, Quaternion.identity);
            Instantiate(_ammoPickUp, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
    public override void state_action()
    {

    }
}
