using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public  class state : MonoBehaviour
{
    public virtual void state_action()
    {
        
    }
    public virtual void on_state_enter()
    {
        changestate = "";
    }
    public string name;
    public string changestate = "";
    public virtual string getstate()
    {
        return name;
    }
}

public class state_machine : MonoBehaviour
{
    public List<state> states;
    public state current_state;
    public virtual void action()
    {
        current_state.state_action();
        if (current_state.changestate != "") state_change(current_state.changestate);
    }
    
    public void state_change(string name)
    {
        for(int i=0;i<states.Count;i++)
        {
            if(states[i].name == name)
            {
                current_state = states[i];
                current_state.on_state_enter();
                break;
            }
        }
    }

}
public class moving_state : state
{
    public float speed;
    public NavMeshAgent nav;
    public override void on_state_enter()
    {
        base.on_state_enter();
        nav.speed = speed;

    }
    public override void state_action()
    {
        base.state_action();
    }
    public string get_state()
    {
        return name;
    }

}
