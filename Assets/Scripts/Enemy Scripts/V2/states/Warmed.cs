using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warmed : state
{
    public List<moving_state> states;
    public state current_state;
    public float shoottime;
    public int shootcount;
    public int shootcount2 = 0;
    public bool mele;

    [SerializeField] private AudioCueSO _warmedAudioCue;
    private AudioCue _audioCue;
    bool surprised = false;
    public void Start()
    {
        states.Add(this.GetComponent<Aiming>());
        states.Add(this.GetComponent<Attacking>());
        states.Add(this.GetComponent<Running>());

        state_change("Running");
        if(this.GetComponent<enemystats>().type == enemystats.enemy_type.Hans)
        {
            shootcount = 1;
        }
        if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Helmut)
        {
            shootcount = 5;
        }

        _audioCue = AudioCueComponent;
    }
    public override void on_state_enter()
    {
        base.on_state_enter();
        if (!surprised)
        {
            _audioCue.AudioData = _warmedAudioCue;
            PlaySound();
            surprised = true;
        }
        state_change("Running");
    }
    public override void state_action()
    {
        
        if (this.gameObject.GetComponent<enemystats>().enemyhealth.CurrentHealth <= 0)
        {
            changestate = "Dead";
        }
        else if (this.gameObject.GetComponent<enemystats>().hurt)
        {
            changestate = "Stunned";

        }
       else  if (!mele)
        {
            if (this.gameObject.GetComponent<Sight>().see || this.GetComponent<enemystats>().getdistance() < 5)
            {
                if (shoottime <= 0)
                {
                    if(shootcount == 1) { shoottime = 2.0f; }
                    if(shootcount -1 == shootcount2) { shootcount2 = 0; shoottime = 2.0f; }
                    else if(shootcount > 1) { shootcount2++; shoottime = 0.2f; }
                    state_change("Attacking");
                    this.gameObject.GetComponentInChildren<Aminations>().anim();
                    System.Random rand = new System.Random();
                    int attack = (int)(rand.Next(1, 10));
                    if (attack < 9)
                    {
                        GameObject p = GameObject.FindGameObjectWithTag("Player");
                        p.GetComponent<HealthManager>().DamagePlayer(this.gameObject.GetComponent<enemystats>().dmg);
                    }

                }
                else
                {
                    state_change("Aiming");
                    shoottime -= Time.deltaTime;
                }
            }
            else
            {
                if (shoottime > 0) shoottime -= Time.deltaTime;
                if (getstate() != "Running") state_change("Running");
                this.gameObject.GetComponent<NavMeshAgent>().destination = this.gameObject.GetComponent<enemystats>().player.position;
            }
        }
        else
        {
            float distance = this.gameObject.GetComponentInParent<enemystats>().getdistance();
            if(distance < 8.0f)
            {
                
                if (shoottime <= 0)
                {
                    GameObject p = GameObject.FindGameObjectWithTag("Player");
                    p.GetComponent<HealthManager>().DamagePlayer(this.gameObject.GetComponent<enemystats>().dmg);
                    shoottime = 1.0f;
                    state_change("Attacking");
                }
                else
                {
                    shoottime -= Time.deltaTime;
                }
            }
            else
            {
                if (shoottime > 0) shoottime -= Time.deltaTime;
                if (getstate() != "Running") state_change("Running");
                this.gameObject.GetComponent<NavMeshAgent>().destination = this.gameObject.GetComponent<enemystats>().player.position;
            }
        }

        if (current_state.changestate != "") state_change(current_state.changestate);
    }
    public void state_change(string name)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].name == name)
            {
                current_state = states[i];
                current_state.on_state_enter();
                break;
            }
        }
    }
    public override string getstate()
    {
        return current_state.name;
    }
}
