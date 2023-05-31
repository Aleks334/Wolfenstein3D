using UnityEngine;

public class Item : AudioPlayable, IInteractableTrigger
{
    [SerializeField] private ItemDataSO _itemData;

    public void Interact()
    {
        if (_itemData.CanBePickedUp())
        {
            _itemData.PickupItem();

            PlaySound();
            UI.healtincreaseeffect();
            Destroy(this.gameObject);
        }
    }
}