using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class bossrunning : moving_state
{
    // Start is called before the first frame update
    private void Start()
    {
        name = "bossrunning";
    }
    public override void on_state_enter()
    {
        this.gameObject.GetComponent<NavMeshAgent>().speed =  this.gameObject.GetComponent<bossstats>().speed;
    }
}
