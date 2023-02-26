using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolpoints : MonoBehaviour
{
    public List<Transform> points;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform getrandompoint()
    {
        System.Random r = new System.Random();
        int x = r.Next(0, points.Count);
        return points[x];
    }
}
