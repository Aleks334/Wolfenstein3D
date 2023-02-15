using UnityEngine;

public class PlayerMvmtInitState : State
{
    public PlayerMvmtInitState(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
        Debug.Log("Entered into PlayerMvmtInitState");
        _playerMovementManager._characterController = _playerMovementManager.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerMovementManager.PlayerStateMachine.ChangeState(_playerMovementManager.WaitForInputState);
    }

    public override void UpdateState()
    {
        Debug.Log("Executing PlayerMvmtInitState every frame");
    }

    public override void ExitState()
    {
        Debug.Log("Exit from PlayerMvmtInitState");

    }
}