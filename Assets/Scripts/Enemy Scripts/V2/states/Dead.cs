using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : state
{
    [SerializeField] private GameObject _ammoPickUp;

    [SerializeField] private GameObject _deadhans;
    [SerializeField] private GameObject _deadhelmut;
    [SerializeField] private GameObject _deaddog;

    [SerializeField] private AudioCueSO _deathAudioCue;
    private AudioCue _audioCue;
    float time;

    private void Start()
    {
        name = "Dead";
        _audioCue = AudioCueComponent;
    }
    public override void on_state_enter()
    {
        _audioCue.AudioData = _deathAudioCue;
        PlaySound();

        this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;

        if (this.gameObject.TryGetComponent<enemystats>(out enemystats enemy))
        {
            time = 1.0f;
            if (enemy.type == enemystats.enemy_type.Doge)
            {
                //�mier� psa
            }
            else if (enemy.type == enemystats.enemy_type.Hans)
            {
                //�mier� zwyk�ego �o�mierza
                Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
            }
            else if (enemy.type == enemystats.enemy_type.Helmut)
            {
                //�mier� SS
                Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
            }
        }
        else
        {
            //�mier� bossa
        }
    }
    public override void state_action()
    {
        if (this.gameObject.TryGetComponent<enemystats>(out enemystats enemy))
        {
            if (time > 0) time -= Time.deltaTime;
            else
            {
                if (enemy.type == enemystats.enemy_type.Doge)
                {
                    //�mier� psa
                    Instantiate(_deaddog, transform.position, Quaternion.identity);
                }
                else if (enemy.type == enemystats.enemy_type.Hans)
                {
                    //�mier� zwyk�ego �o�mierza
                    Instantiate(_deadhans, transform.position, Quaternion.identity);
                }
                else if (enemy.type == enemystats.enemy_type.Helmut)
                {
                    //�mier� SS
                    Instantiate(_deadhelmut, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
