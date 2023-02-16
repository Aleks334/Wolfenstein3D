using UnityEngine;

public class StandingState : Grounded
{
    public StandingState(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM) { }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isRunning)
        {
            _fsm.ChangeState(_movementManager.Running);
        }
        else if (isStrafing)
        {
            _fsm.ChangeState(_movementManager.Strafing);
        }
    }
}
