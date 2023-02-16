using UnityEngine;

public class Grounded : State
{
    public Grounded(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM) { }

    protected float mvmtSpeed = 12f;
    protected float runningRate = 1.8f;
    protected float strafingRate = 1.4f;

    protected Vector3 inputVector;
    protected Vector3 movementVector;
    protected bool isRunning;
    protected bool isStrafing;

    protected float inputRotation;
    protected float sensitivity = 110f;

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
        RotatePlayer();

        MovePlayerVertical();

        isRunning = Input.GetKey(KeyCode.LeftShift);
        isStrafing = Input.GetKey(KeyCode.LeftAlt);

        //for viewing values in game
        _movementManager.inputVector = inputVector;
        _movementManager.movementVector = movementVector;
        _movementManager.isRunning = isRunning;
        _movementManager.isStrafing = isStrafing;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _movementManager.CharacterController.Move(movementVector * Time.deltaTime);
    }





    void MovePlayerVertical()
    {
        inputVector = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = _movementManager.gameObject.transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
    }

    void RotatePlayer()
    {
        inputRotation = Input.GetAxisRaw("Horizontal");
        inputRotation *= sensitivity;
        _movementManager.gameObject.transform.Rotate(0f, inputRotation * Time.deltaTime, 0f);
    }
}
