using UnityEngine;

public abstract class State
{
    protected readonly GameManager _gameManager;
    protected readonly object _controller;

    public State(object controller)
    {
        _controller = controller;
    }

    public abstract void EnterState();
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void ExitState() { }
}