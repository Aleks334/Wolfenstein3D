using UnityEngine;

public class Item : MonoBehaviour, IInteractableTrigger, IAudio
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

    public void PlaySound()
    {
        if (TryGetComponent<AudioCue>(out AudioCue audioCue))
            audioCue.PlayAudioCue();
    }
}
