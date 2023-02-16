using UnityEngine;

public class StrafingState : Grounded
{
    public StrafingState(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
 
    }

    public override void HandleInput()
    {
        _movementManager.IsStrafing = Input.GetKeyDown(KeyCode.LeftAlt);
    }

    public override void LogicUpdate()
    {
        MovePlayerHorizontal();

        if(!_movementManager.IsStrafing)
        {
            _movementManager.PlayerMovementFSM.ChangeState(_movementManager.Standing);
        }
    }

    void MovePlayerHorizontal()
    {
        _movementManager.inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        _movementManager.inputVector.Normalize();
        _movementManager.inputVector = _movementManager.gameObject.transform.TransformDirection(_movementManager.inputVector);
        _movementManager.movementVector = (_movementManager.inputVector * _movementManager.mvmtSpeed);
    }
}
