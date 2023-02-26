using UnityEngine;

public class StandingState : Grounded
{
    public StandingState(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM)
    {
        //Assigning values from PlayerMovementSettings SO to Grounded in each of state
        base.mvmtSpeed = _movementManager.MvmtSettings.mvmtSpeed;
        base.strafingRate = _movementManager.MvmtSettings.strafingRate;
        base.runningRate = _movementManager.MvmtSettings.runningRate;
        base.sensitivity = _movementManager.MvmtSettings.sensitivity;
    }

    #region Methods for FSM Update loop

    public override void EnterState()
    {
        base.EnterState();
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
    #endregion
}
