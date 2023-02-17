using UnityEngine;

public class Grounded : State
{
    public Grounded(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM) { }

    #region Fields and Properties

    public float mvmtSpeed;

    public float runningRate;
    public float strafingRate;

    public Vector3 inputVector;
    public Vector3 movementVector;
    public bool isRunning;
    public bool isStrafing;

    public float inputRotation;
    public float sensitivity;

    #endregion

    #region Methods for FSM Update loop
    public override void HandleInput()
    {
        base.HandleInput();

        HandleInputRotation();
        HandleInputVertical();

        isRunning = Input.GetKey(KeyCode.LeftShift);
        isStrafing = Input.GetKey(KeyCode.LeftAlt);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _movementManager.CharacterController.Move(movementVector * Time.deltaTime);
    }

    #endregion

    #region Methods for logic

    void HandleInputVertical()
    {
        inputVector = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = _movementManager.gameObject.transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
    }

    void HandleInputRotation()
    {
        inputRotation = Input.GetAxisRaw("Horizontal");
        inputRotation *= sensitivity;
        _movementManager.gameObject.transform.Rotate(0f, inputRotation * Time.deltaTime, 0f);
    }

    #endregion
}
