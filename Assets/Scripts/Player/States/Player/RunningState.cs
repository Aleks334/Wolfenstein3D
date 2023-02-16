using UnityEngine;

public class RunningState : Grounded
{
    public RunningState(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
        _movementManager.movementVector *= _movementManager.runningRate;
    }

    public override void HandleInput()
    {
        _movementManager.IsRunning = Input.GetKey(KeyCode.LeftShift);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_movementManager.IsRunning)
        {
            _movementManager.PlayerMovementFSM.ChangeState(_movementManager.Standing);
        }
    }
}
