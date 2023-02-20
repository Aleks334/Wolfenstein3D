using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDataSO _itemData;

    public void Interact()
    {
        if (_itemData.CanBePickedUp())
        {
            _itemData.PickupItem();
            UI.healtincreaseeffect();
            Destroy(this.gameObject);
        }
    }
}
