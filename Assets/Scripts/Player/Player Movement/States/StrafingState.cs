using UnityEngine;

public class StrafingState : Grounded
{
    public StrafingState(PlayerMovementManager playerMovementManager, StateMachine FSM) : base(playerMovementManager, FSM)
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
    #endregion

    #region Methods for FSM Update loop
    public override void HandleInput()
    {
        MovePlayerWithStrafing();

        isStrafing = Input.GetKey(KeyCode.LeftAlt);
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isStrafing)
            _fsm.ChangeState(_movementManager.Standing);
    }
    #endregion

    #region Methods for logic
    void MovePlayerWithStrafing()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = _movementManager.gameObject.transform.TransformDirection(inputVector);
        movementVector = (inputVector * mvmtSpeed);
       
    }
    #endregion
}
