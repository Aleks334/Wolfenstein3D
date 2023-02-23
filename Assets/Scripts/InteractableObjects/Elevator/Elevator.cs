using UnityEngine;

public class Elevator : MonoBehaviour, IInteractableRaycast
{
    public void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.Victory);
    }
}
