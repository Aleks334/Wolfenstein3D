using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats/Health_Stats")]
public class HealthSO : ScriptableObject
{
    public ObjectHealth HealthData { get; private set; }

    [SerializeField] private int _defaultAmount;
    [SerializeField] private int _maxAmount;

    public void OnEnable()
    {
        HealthData = new ObjectHealth(_defaultAmount, _maxAmount);
    }

    public void ResetHealth()
    {
        HealthData.HealingValue(HealthData.MaxHealth);
    }

    public bool IsAlive()
    {
        if (HealthData.CurrentHealth == 0)
            return false;
        else
            return true;
    }
}