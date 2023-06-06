using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : state
{
    [SerializeField] private GameObject _ammoPickUp;
    [SerializeField] private GameObject _deadhans;
    [SerializeField] private GameObject _deadhelmut;
    [SerializeField] private GameObject _deaddog;
    float time;
    private void Start()
    {
        name = "Dead";
    }
    public override void on_state_enter()
    {
        if (this.gameObject.TryGetComponent<enemystats>(out enemystats enemy))
        {
            time = 1.0f;
            if (enemy.type == enemystats.enemy_type.Doge)
            {
                //œmieræ psa
            }
            else if (enemy.type == enemystats.enemy_type.Hans)
            {
                //œmieræ zwyk³ego ¿o³mierza
                Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
            }
            else if (enemy.type == enemystats.enemy_type.Helmut)
            {
                //œmieræ SS
                Instantiate(_ammoPickUp, transform.position, Quaternion.identity);
            }
        }
        else
        {
            //œmieræ bossa
        }
    }
    public override void state_action()
    {
        if (this.gameObject.TryGetComponent<enemystats>(out enemystats enemy))
        {
            if (time > 0) time -= Time.deltaTime;
            else
            {
                if (enemy.type == enemystats.enemy_type.Doge)
                {
                    //œmieræ psa
                    Instantiate(_deaddog, transform.position, Quaternion.identity);
                }
                else if (enemy.type == enemystats.enemy_type.Hans)
                {
                    //œmieræ zwyk³ego ¿o³mierza
                    Instantiate(_deadhans, transform.position, Quaternion.identity);
                }
                else if (enemy.type == enemystats.enemy_type.Helmut)
                {
                    //œmieræ SS
                    Instantiate(_deadhelmut, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
