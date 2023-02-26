using UnityEngine;

//Universal class for every object that can become a finite state machine.
public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }


    public void ChangeState(State newState)
    {
        CurrentState.ExitState();

        CurrentState = newState;
        CurrentState.EnterState();
    }
}