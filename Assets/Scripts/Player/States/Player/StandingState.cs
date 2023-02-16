using Unity.VisualScripting;
using UnityEngine;

public class StandingState : Grounded
{
    public StandingState(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
       // base.EnterState();
   //     speed = character.MovementSpeed;
   //    rotationSpeed = character.RotationSpeed;
   //     crouch = false;
   //     jump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        _movementManager.IsRunning = Input.GetKey(KeyCode.LeftShift);
        _movementManager.IsStrafing = Input.GetKeyDown(KeyCode.LeftAlt);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_movementManager.IsRunning)
        {
            Debug.Log("!!!!!");
            _movementManager.PlayerMovementFSM.ChangeState(_movementManager.Running);
        }
        else if (_movementManager.IsStrafing)
        {
            _movementManager.PlayerMovementFSM.ChangeState(_movementManager.Strafing);
        }
    }
}
