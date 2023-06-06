using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : moving_state
{
    // Start is called before the first frame update
    public void Start()
    {
        name = "Attacking";
        speed = 0;
        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }
}
