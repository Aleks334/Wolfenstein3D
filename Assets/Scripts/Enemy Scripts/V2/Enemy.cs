using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : state_machine
{
    bool isset = false;
    public string state;
    public delegate void Action_delegate();
    Action_delegate handler;
    public void Start()
    {
        states.Add(this.GetComponent<Patroling>());
        states.Add(this.GetComponent<Warmed>());
        states.Add(this.GetComponent<Stunned>());
        states.Add(this.GetComponent<Dead>());
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isset) { state_change("Patroling"); isset = true; Recover_enemy(); }
        handler();
        state = getstate();
    }
    public override void action()
    {
        
        base.action();

    }
    public void no_action()
    {
        //do nothing you are stopped
    }
    public string getstate()
    {
        return current_state.getstate();
    }
    public void Stop_enemy()
    {
        handler = no_action;
    }
    public void Recover_enemy()
    {
        handler = action;
    }
}
