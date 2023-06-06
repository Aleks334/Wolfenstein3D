using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossstunned : state
{
    // Start is called before the first frame update
    public float stuntime = 0;
    private void Start()
    {
        this.GetComponent<bossstats>().Mehdestroyed += OnMehdestroyed;
        name = "bossstunned";
    }
    public override void on_state_enter()
    {
        this.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        this.gameObject.GetComponent<bossstats>().hurt = false;

        stuntime = 0.5f;

    }
    public override void state_action()
    {
        stuntime -= Time.deltaTime;
        if (stuntime <= 0)
        {

            changestate = "bossattack";
        }
    }
    public void OnMehdestroyed()
    {
        stuntime = 1.0f;
    }
}
