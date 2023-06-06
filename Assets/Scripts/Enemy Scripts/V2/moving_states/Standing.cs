using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Standing : moving_state
{
    // Start is called before the first frame update
    public void Start()
    {
        name = "Standing";
        speed = 0;
        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }
}
