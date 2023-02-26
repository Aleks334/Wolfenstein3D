using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stunned : state
{
   public  float stuntime = 0;
    public override void on_state_enter()
    {
        this.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        this.gameObject.GetComponent<enemystats>().hurt = false;
        
        stuntime = 0.5f;

    }
    public override void state_action()
    {
        stuntime -= Time.deltaTime;
        if (stuntime <= 0)
        {
            
            changestate = "Warmed";
        }
    }
}
