using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossattack : state
{
    public List<moving_state> states;
    public state current_state;
    public float shoottime = 1.5f;
    int shootcount;
    public int shoots = 0;
    int dmgpow = 2;
    public void Start()
    {
        states.Add(this.GetComponent<Aiming>());
        states.Add(this.GetComponent<Attacking>());
        states.Add(this.GetComponent<bossrunning>());
        state_change("bossrunning");
        shootcount = this.GetComponent<bossstats>().shootcount;
        this.GetComponent<bossstats>().Mehdestroyed += OnMehdestroyed;
        name = "bossattack";
    }
    public override void on_state_enter()
    {
        base.on_state_enter();
        state_change("bossrunning");

    }
    public override void state_action()
    {
        if (this.GetComponent<Sight>().see)
        {
            if (shoottime > 0)
            {
                aim();
                state_change("Aiming");
                shoottime -= Time.deltaTime;
            }
            else if (shoottime <= 0)
            {
                shoot();
                state_change("Attacking");
                shoottime = 0.2f;
                shoots++;
                if(shootcount == shoots)
                {
                    shoottime = 2.0f;
                    shoots = 0;
                }
                System.Random rand = new System.Random();
                int chance = rand.Next(1, 10);
                if(chance > 5)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().DamagePlayer(this.GetComponent<bossstats>().dmg * dmgpow);
                }
                else if(chance > 1)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().DamagePlayer(this.GetComponent<bossstats>().dmg);
                }

            }
        }
        else
        {
            run();
            state_change("bossrunning");
            this.gameObject.GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        }
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
    public void OnMehdestroyed()
    {
        dmgpow--;
    }
    public event Action shoot;
    public event Action run;
    public event Action aim;

}
