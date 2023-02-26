using System.Runtime.CompilerServices;
using UnityEngine;

public class ElevatorLever : MonoBehaviour, IInteractableRaycast
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
        _renderer.material = elevatorLeverEnabledMat;
        _onPlayerVictory.RaiseEvent();
      //  GameManager.Instance.UpdateGameState(GameState.Victory);
    }
}
