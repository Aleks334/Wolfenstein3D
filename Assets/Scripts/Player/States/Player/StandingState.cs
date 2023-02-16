using UnityEngine;

public class StandingState : Grounded
{
    public StandingState(PlayerMovementManager playerMovementManager) : base(playerMovementManager) { }

    public override void EnterState()
    {
        base.EnterState();
   //     speed = character.MovementSpeed;
   //    rotationSpeed = character.RotationSpeed;
   //     crouch = false;
   //     jump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
     //   crouch = Input.GetButtonDown("Fire3");
    //    jump = Input.GetButtonDown("Jump");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
     /*   if (crouch)
        {
            stateMachine.ChangeState(character.ducking);
        }
        else if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }*/
    }
}
