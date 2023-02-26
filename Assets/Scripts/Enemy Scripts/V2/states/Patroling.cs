using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patroling : state
{

    public List<state> states;
    public state current_state;
    float walktime;
    public patrolpoints p;
    System.Random random = new System.Random();
    public void Start()
    {

    }
    public override void on_state_enter()
    {
        states.Add(this.GetComponent<Standing>());
        states.Add(this.GetComponent<Walking>());
        walktime = 0.0f;
        base.on_state_enter();
        state_change("Standing");
    }
    public override void state_action()
    {
        if(this.gameObject.GetComponent<enemystats>().enemyhealth.CurrentHealth <=0)
        {

            this.gameObject.GetComponent<Enemy>().state_change("Dead");
        }
        else if(this.gameObject.GetComponent<enemystats>().hurt)
        {
            this.gameObject.GetComponent<Enemy>().state_change("Stunned");

        }
        else if (this.gameObject.GetComponent<Sight>().see)
        {
            changestate = "Warmed";
        }
        else if (((int)GameManager.PlayerObj.GetComponent< PlayerMovementManager>().PlayerNoiseLevel * (int)(GameManager.PlayerObj.GetComponent<PlayerMovementManager>().PlayerNoiseLevel)) / this.gameObject.GetComponent<enemystats>().getdistance() > this.gameObject.GetComponent<enemystats>().noisevalue)
        {
            changestate = "Warmed";
        }
        else
        {
            Debug.Log(((int)GameManager.PlayerObj.GetComponent<PlayerMovementManager>().PlayerNoiseLevel * (int)(GameManager.PlayerObj.GetComponent<PlayerMovementManager>().PlayerNoiseLevel)) / this.gameObject.GetComponent<enemystats>().getdistance());
            
            if (walktime <= 0)
            {
                if (current_state.name == "Walking" && !this.gameObject.GetComponent<enemystats>().mele)
                {
                    state_change("Standing");
                    walktime = (float)(random.Next(1, 3));
                }
                else
                {
                    state_change("Walking");
                    //float x = this.transform.position.x + this.transform.forward.x * random.Next(-30, 30) + this.transform.right.x * random.Next(-30, 30);
                    //float z = this.transform.position.z + this.transform.forward.z * random.Next(-30, 30) + this.transform.right.z * random.Next(-30, 30);
                    Transform x = p.getrandompoint();
                    Debug.Log(x.position);
                    Vector3 direction = new Vector3(x.position.x, this.transform.position.y, x.position.z);
                    this.GetComponent<NavMeshAgent>().destination = direction;
                    walktime = (float)(random.Next(5, 20));
                }
            }
            else
            {
                walktime -= Time.deltaTime;
            }
        }
    }
    public void state_change(string name)
    {

        for (int i = 0; i < states.Count; i++)
        {

            if (states[i].name == name)
            {
                

                current_state = states[i];
                current_state.on_state_enter();
                break;
            }
        }
    }
    public override string getstate()
    {
        return current_state.getstate();
    }
}
