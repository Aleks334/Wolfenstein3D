using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : state
{
    [SerializeField] private GameObject _ammoPickUp;

    public override void on_state_enter()
    {
        this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        if(this.GetComponent<enemystats>().type == enemystats.enemy_type.Doge)
        {
            //œmieræ psa
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Hans)
        {
            //œmieræ zwyk³ego ¿o³mierza
            Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Helmut)
        {
            //œmieræ SS
            Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
        }
    }
    public override void state_action()
    {

    }
}
