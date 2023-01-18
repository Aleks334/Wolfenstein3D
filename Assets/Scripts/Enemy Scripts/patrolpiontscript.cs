using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolpiontscript : MonoBehaviour
{
    //funkcja obs�uguj�ca punkty do patrolowania przezniezaalarmowanych przeciwnik�w, je�eli przeciwnik dojdzie do tego punktu to zaczyna si� kierowa� w stron� nast�pnego

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Enemynevmesh>() != null)
        {
            if (other.transform.GetComponent<Enemynevmesh>().destination.position == transform.position)
            {
                other.transform.GetComponent<EnemyManager>().onrightplace = true;
            }
        }
    }
}
