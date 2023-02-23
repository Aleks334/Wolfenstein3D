using UnityEngine;

public class Elevator : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.UpdateGameState(GameState.Victory);
    }
}
