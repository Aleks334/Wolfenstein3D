using UnityEngine;

public abstract class ItemDataSO : ScriptableObject
{
    public abstract void PickupItem(GameObject player);

    public abstract bool CanBePickedUp(GameObject player);
}