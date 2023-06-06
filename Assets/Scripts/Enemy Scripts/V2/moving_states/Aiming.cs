using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Aiming : moving_state
{
    public void Start()
    {
        name = "Aiming";
        speed = 0.0f;
        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }

}
