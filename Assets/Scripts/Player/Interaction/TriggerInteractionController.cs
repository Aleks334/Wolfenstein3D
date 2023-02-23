using UnityEngine;

public class TriggerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractableTrigger>(out IInteractableTrigger obj))
            obj.Interact();
    }
}