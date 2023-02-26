using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : state
{
    public override void on_state_enter()
    {
        if(this.GetComponent<enemystats>().type == enemystats.enemy_type.Doge)
        {
            //œmieræ psa
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Hans)
        {
            //œmieræ zwyk³ego ¿o³mierza
        }
        else if (this.GetComponent<enemystats>().type == enemystats.enemy_type.Helmut)
        {
            //œmieræ SS
        }
    }
    public override void state_action()
    {

    }
}
