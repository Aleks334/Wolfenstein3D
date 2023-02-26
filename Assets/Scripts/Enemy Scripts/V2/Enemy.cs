using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : state_machine
{
    bool isset = false;
    public string state;

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
        if (!isset) { state_change("Patroling"); isset = true; }
        action();
        state = getstate();
    }
    public override void action()
    {
        
        base.action();

    }
    public string getstate()
    {
        return current_state.getstate();
    }
}
