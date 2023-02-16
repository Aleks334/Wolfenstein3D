using UnityEngine;

public class Grounded : State
{
    public Grounded(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
      //  horizontalInput = verticalInput = 0.0f;
    }

    public override void ExitState()
    {
      //  _controller.ResetMoveParams();
    }

    public override void HandleInput()
    {
       // verticalInput = Input.GetAxis("Vertical");
       // horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void PhysicsUpdate()
    {
      //  base.PhysicsUpdate();
       // character.Move(verticalInput * speed, horizontalInput * rotationSpeed);
    }
}
