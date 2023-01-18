using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolpiontscript : MonoBehaviour
{
    //funkcja obs³uguj¹ca punkty do patrolowania przezniezaalarmowanych przeciwników, je¿eli przeciwnik dojdzie do tego punktu to zaczyna siê kierowaæ w stronê nastêpnego

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
