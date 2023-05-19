using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats/Health_Stats")]
public class PlayerHealthSO : ScriptableObject
{
    public ObjectHealth playerHealth;

    [SerializeField] private int _defaultAmount;
    [SerializeField] private int _maxAmount;

    [HideInInspector] public bool JustDied = false;

    public void OnEnable()
    {
        playerHealth = new ObjectHealth(_defaultAmount, _maxAmount);
    }

    public void GiveDefaultHealth()
    {
        playerHealth.HealingValue(playerHealth.MaxHealth);
    }

    public bool isAlive()
    {
        if (playerHealth.CurrentHealth == 0)
            return false;
        else
            return true;
    }
}