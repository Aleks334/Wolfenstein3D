using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossstats : MonoBehaviour
{
    public ObjectHealth bosshealth;
    public Transform me;
    public float speed;
    public int dmg;
    public bool hurt = false;
    public float noisevalue;
    public int hp;
    public bool meh = true;
    public bool dead = false;
    public int shootcount;

    public Transform player;
    public void Start()
    {
        bosshealth = new ObjectHealth(hp, hp);
        me = this.transform;

    }

    public void Dmgenemy(int value)
    {
        
        bosshealth.DmgValue(value);
        bosshurt();
        if (meh && bosshealth.CurrentHealth <=hp/2)
        {
            Debug.Log("xy");
            meh = false;
            Mehdestroyed();
        }
        if(!dead && bosshealth.CurrentHealth <=0)
        {
            Debug.Log("x");
            dead = true;
            bossdie();
        }
        
    }
    public float getdistance()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        return (Mathf.Pow(this.transform.position.x - player.position.x, 2) + Mathf.Pow(this.transform.position.z - player.position.z, 2));
    }
    public event Action Mehdestroyed;
    public event Action bossdie;
    public event Action bosshurt;
}
