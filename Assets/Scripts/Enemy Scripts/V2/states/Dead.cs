using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : state
{
    public override void on_state_enter()
    {
        if(this.GetComponent<enemystats>().type == enemystats.enemy_type.Doge)
        {
            //�mier� psa
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Hans)
        {
            //�mier� zwyk�ego �o�mierza
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Helmut)
        {
            //�mier� SS
        }
    }
    public override void state_action()
    {

    }
}
