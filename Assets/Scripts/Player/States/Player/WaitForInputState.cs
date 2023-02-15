using UnityEngine;

public class WaitForInputState : State
{
    public WaitForInputState(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
        Debug.Log("Entered into WaitForInputState");
    }

    public override void UpdateState()
    {
        Debug.Log("Executing WaitForInputState every frame");
    }

    public override void ExitState()
    {
        Debug.Log("Exit from WaitForInputState");

    }
}
