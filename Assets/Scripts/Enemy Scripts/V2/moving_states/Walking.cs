using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walking : moving_state
{
    // Start is called before the first frame update
    public void Start()
    {
        
        speed = this.gameObject.GetComponent<enemystats>().speed;
        nav = this.gameObject.GetComponent<NavMeshAgent>();
    }
}
