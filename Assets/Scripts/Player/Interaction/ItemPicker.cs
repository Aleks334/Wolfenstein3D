using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable item))
            item.Interact(this.gameObject);
    }
}