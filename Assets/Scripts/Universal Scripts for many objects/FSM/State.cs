using UnityEngine;

public abstract class State
{
    protected readonly PlayerMovementManager _movementManager;
    protected readonly StateMachine _fsm;

    public State(PlayerMovementManager controller, StateMachine FSM)
    {
        _movementManager = controller;
        _fsm = FSM;
    }

    public virtual void EnterState()
    {
       // Debug.Log("Entered into " + this);
    }
    public virtual void HandleInput()
    {
       // Debug.Log("HandleInput in " + this);
    }
    public virtual void LogicUpdate()
    {
       // Debug.Log("Updating Logic in " + this);
    }
    public virtual void PhysicsUpdate() { }
    public virtual void ExitState()
    {
      //  Debug.Log("Exited from " + this);
    }
}

public class Controller<T>
{
    public T Data { get; private set; }
}