using UnityEngine;

public abstract class State
{
    protected readonly PlayerMovementManager _movementManager;

    public State(PlayerMovementManager controller)
    {
        _movementManager = controller;
    }

    public abstract void EnterState();
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void ExitState() { }
}