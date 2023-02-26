using UnityEngine;

public abstract class ItemDataSO : ScriptableObject
{
    public abstract void PickupItem();

    public abstract bool CanBePickedUp();

    protected abstract void FindNeededManager();
}