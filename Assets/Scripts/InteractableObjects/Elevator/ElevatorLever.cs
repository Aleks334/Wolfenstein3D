using UnityEngine;

public class ElevatorLever : AudioPlayable, IInteractableRaycast
{
    private Renderer _renderer;
    [SerializeField] private Material elevatorLeverEnabledMat;

    [SerializeField] private VoidEventChannelSO _onPlayerVictory;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        PlaySound();
        _renderer.material = elevatorLeverEnabledMat;
        _onPlayerVictory.RaiseEvent();
    }
}
