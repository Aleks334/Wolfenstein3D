using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats/Health_Stats")]
public class PlayerHealthSO : ScriptableObject
{
    public ObjectHealth playerHealth;

    [SerializeField] private int _defaultAmount;
    [SerializeField] private int _maxAmount;

    private void OnEnable()
    {
        playerHealth = new ObjectHealth(_defaultAmount, _maxAmount);
    }

    public void GiveMaxHealth()
    {
        playerHealth.HealingValue(playerHealth.MaxHealth);
    }
}