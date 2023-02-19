using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDataSO _itemData;

    public void Interact(GameObject player)
    {
        if(_itemData.CanBePickedUp(player))
        {
            _itemData.PickupItem(player);
            UI.healtincreaseeffect();
            Destroy(this.gameObject);
        }
    }
}
