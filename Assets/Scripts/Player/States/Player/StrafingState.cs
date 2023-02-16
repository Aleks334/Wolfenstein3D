using UnityEngine;

public class StrafingState : Grounded
{
    public StrafingState(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM) { }

    private float defaultMvmtSpeed;

    public override void EnterState()
    {
        base.EnterState();

        isStrafing = true;

        defaultMvmtSpeed = mvmtSpeed;
        mvmtSpeed *= strafingRate;
    }

    public override void ExitState()
    {
        base.ExitState();

        mvmtSpeed = defaultMvmtSpeed;
        isStrafing = false;
    }

    public override void HandleInput()
    {
        MovePlayerWithStrafing();

        isStrafing = Input.GetKey(KeyCode.LeftAlt);
        isRunning = Input.GetKey(KeyCode.LeftShift);

        //for viewing values in game
        _movementManager.inputVector = inputVector;
        _movementManager.movementVector = movementVector;
        _movementManager.isStrafing = isStrafing;
        _movementManager.isRunning = isRunning;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isStrafing)
            _fsm.ChangeState(_movementManager.Standing);
    }





    void MovePlayerWithStrafing()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = _movementManager.gameObject.transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
       
    }
}
