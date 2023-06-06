using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss : state_machine
{
    bool isset = false;
    public string state;
    public float time;
    string spawnstring = "";
    [SerializeField] private GameObject _destroyedmeh;
    [SerializeField] private GameObject _deadboss;
    public void Start()
    {
        states.Add(this.GetComponent<bossPatroling>());
        states.Add(this.GetComponent<bossstunned>());
        states.Add(this.GetComponent<Dead>());
        states.Add(this.GetComponent<bossattack>());
        this.GetComponent<bossstats>().Mehdestroyed += OnMehDestroyed;
        this.GetComponent<bossstats>().bossdie += OnBossDead;

    }

    // Update is called once per frame
    void Update()
    {
        if(time >0)
        {
            time -= Time.deltaTime;
        }
        else if(spawnstring == "meh")
        {
            Instantiate(_destroyedmeh, transform.position, Quaternion.identity);
            spawnstring = "";
        }
        else if(spawnstring == "boss")
        {
            Instantiate(_deadboss, transform.position, Quaternion.identity);
            spawnstring = "";
            Destroy(this.gameObject);
        }
        if (!isset) { state_change("bossPatroling"); isset = true; }
        action();
        state = getstate();
    }
    public override void action()
    {

        base.action();

    }
    public string getstate()
    {
        return current_state.getstate();
    }
    public void OnMehDestroyed()
    {
        time = 0.6f;
        spawnstring = "meh";
        
    }
    public void OnBossDead()
    {
        
        time = 12.0f;
        spawnstring = "boss";
        state_change("Dead");
    }
}