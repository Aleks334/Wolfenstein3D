using UnityEngine;

public class ElevatorLever : MonoBehaviour, IInteractableRaycast
{
    private Renderer _renderer;
    [SerializeField] private Material elevatorLeverEnabledMat;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        _renderer.material = elevatorLeverEnabledMat;
        GameManager.Instance.UpdateGameState(GameState.Victory);
    }
}
