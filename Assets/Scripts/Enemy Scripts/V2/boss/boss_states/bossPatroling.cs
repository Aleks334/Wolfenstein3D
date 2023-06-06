using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class bossPatroling : state
{
    
    public List<state> states;
    public state current_state;
    float walktime;
    public patrolpoints p;
    System.Random random = new System.Random();
    public void Start()
    {
        name = "bossPatroling";
        this.GetComponent<bossstats>().bosshurt += bosshurt;
    }
    
    public override void on_state_enter()
    {
        states.Add(this.GetComponent<Standing>());
        walktime = 0.0f;
        base.on_state_enter();
        state_change("Standing");
    }
    public override void state_action()
    {
        if (this.gameObject.GetComponent<Sight>().see)
        {
            changestate = "bossattack";
        }
        else if (((int)GameManager.PlayerObj.GetComponent<PlayerMovementManager>().PlayerNoiseLevel * (int)(GameManager.PlayerObj.GetComponent<PlayerMovementManager>().PlayerNoiseLevel)) / this.gameObject.GetComponent<bossstats>().getdistance() > this.gameObject.GetComponent<bossstats>().noisevalue)
        {
            changestate = "bossattack";
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
        return current_state.getstate();
    }
    public void bosshurt()
    {
        changestate = "bossattack";
    }
}