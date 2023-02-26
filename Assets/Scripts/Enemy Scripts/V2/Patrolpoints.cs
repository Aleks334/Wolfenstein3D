using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolpoints : MonoBehaviour
{
     public List<Transform> patrolpoints;
    void Start()
    {
        Debug.Log(patrolpoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public Transform getrandompoint()
    {
        System.Random r = new System.Random();
        int x = r.Next(0, patrolpoints.Count - 1);
        return patrolpoints[x];
    }
}
