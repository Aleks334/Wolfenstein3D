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

    public override void LogicUpdate()
    {
        RotatePlayer();
        MovePlayerVertical();

        _movementManager._characterController.Move(_movementManager.movementVector * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
      //  base.PhysicsUpdate();
       // character.Move(verticalInput * speed, horizontalInput * rotationSpeed);
    }

    void MovePlayerVertical()
    {
        _movementManager.inputVector = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
        _movementManager.inputVector.Normalize();
        _movementManager.inputVector = _movementManager.gameObject.transform.TransformDirection(_movementManager.inputVector);
        _movementManager.movementVector = (_movementManager.inputVector * _movementManager.mvmtSpeed);
    }

    void RotatePlayer()
    {
        _movementManager.inputRotation = Input.GetAxisRaw("Horizontal");
        _movementManager.inputRotation *= _movementManager.sensitivity;
        _movementManager.gameObject.transform.Rotate(0f, _movementManager.inputRotation * Time.deltaTime, 0f);
    }
}
