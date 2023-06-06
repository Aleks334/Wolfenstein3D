using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : moving_state
{
    [SerializeField] private AudioCueSO _attackAudioCue;
    private AudioCue _audioCue;

    public void Start()
    {
        name = "Attacking";
        speed = 0;
        nav = this.gameObject.GetComponent<NavMeshAgent>();

        _audioCue = AudioCueComponent;
    }

    public override void on_state_enter()
    {
        base.on_state_enter();

        _audioCue.AudioData = _attackAudioCue;
        PlaySound();
    }
}
