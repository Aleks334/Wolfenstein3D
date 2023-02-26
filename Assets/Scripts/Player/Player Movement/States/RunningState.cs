using UnityEngine;

public class RunningState : Grounded
{
    public RunningState(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM)
    {
        //Assigning values from PlayerMovementSettings SO to Grounded in each of state
        base.mvmtSpeed = _movementManager.MvmtSettings.mvmtSpeed;
        base.strafingRate = _movementManager.MvmtSettings.strafingRate;
        base.runningRate = _movementManager.MvmtSettings.runningRate;
        base.sensitivity = _movementManager.MvmtSettings.sensitivity;
    }

    private float defaultMvmtSpeed;

    #region Enter and Exit from FSM State
    public override void EnterState()
    {
        base.EnterState();

        defaultMvmtSpeed = mvmtSpeed;
        mvmtSpeed *= runningRate;

        _movementManager.PlayerNoiseLevel = PlayerNoiseLevel.Running;
    }

    public override void ExitState()
    {
        base.ExitState();

        mvmtSpeed = defaultMvmtSpeed;
        isRunning = false;
    }
    #endregion

    #region Methods for FSM Update loop
    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isRunning)
            _fsm.ChangeState(_movementManager.Standing);
    }
    #endregion
}
