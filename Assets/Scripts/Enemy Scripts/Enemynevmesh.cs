using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//klasa NavMeshAgent dla przeciwnika tutaj przeciwnik porusza sie w kierunku punktu wybranego przez swoje AI()
public class Enemynevmesh : MonoBehaviour
{

    [SerializeField]
    public Transform destination;
    [SerializeField]
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // destination = GameObject.Find("Enemy destination(test)").transform;
    }


    void Update()
    {

            move(destination);
        //agent.destination = destination.position;
        Debug.DrawRay(transform.position, transform.forward);


    }
    public void move(Transform dir)
    {
        agent.destination = dir.position;



    }
    

}
