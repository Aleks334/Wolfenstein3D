using UnityEngine;

public abstract class State
{
    protected readonly GameManager _gameManager;
    protected readonly PlayerMovementManager _playerMovementManager;

    public State(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public State(PlayerMovementManager playerMovementManager)
    {
        _playerMovementManager = playerMovementManager;
    }

    public abstract void EnterState();
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}