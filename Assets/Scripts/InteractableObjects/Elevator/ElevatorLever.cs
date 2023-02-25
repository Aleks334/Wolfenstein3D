using UnityEngine;

public class ElevatorLever : MonoBehaviour, IInteractableRaycast
{
    public void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.Victory);
    }
}
