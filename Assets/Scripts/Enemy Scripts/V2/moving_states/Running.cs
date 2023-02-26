using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Running : moving_state
{
    // Start is called before the first frame update
    public void Start()
    {
        
        speed = this.gameObject.GetComponent<enemystats>().speed*2;
        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }
}
