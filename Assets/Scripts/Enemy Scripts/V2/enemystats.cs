using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemystats : MonoBehaviour
{
    public ObjectHealth enemyhealth;
    public Transform me;
    public bool mele;
    public float speed;
    public int dmg;
    public bool hurt = false;
    public float noisevalue;
    public int hp;
    public Transform player;
    public enemy_type type;
    public void Start()
    {
        enemyhealth = new ObjectHealth(hp, hp);
        me = this.transform;
        
    }
    public void Dmgenemy(int value)
    {

        enemyhealth.DmgValue(value);
        hurt = true;
    }
    public float getdistance()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        return (Mathf.Pow(this.transform.position.x - player.position.x, 2) + Mathf.Pow(this.transform.position.z - player.position.z, 2));
    }
    public enum enemy_type
    {
        Doge,
        Hans, //normal soldier
        Helmut //SS
    }
}
