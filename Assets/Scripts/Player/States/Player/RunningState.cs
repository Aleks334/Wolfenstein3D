using UnityEngine;

public class RunningState : Grounded
{
    public RunningState(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM) { }

    private float defaultMvmtSpeed;
    public override void EnterState()
    {
        base.EnterState();

        defaultMvmtSpeed = mvmtSpeed;
        mvmtSpeed *= runningRate;
    }

    public override void ExitState()
    {
        base.ExitState();

        mvmtSpeed = defaultMvmtSpeed;
        isRunning = false;
    }

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
}
