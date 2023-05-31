public class ObjectHealth
{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public ObjectHealth(int health, int maxHealth)
    {
        CurrentHealth = health;
        MaxHealth = maxHealth;
    }

    public void DmgValue(int dmgAmount)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth -= dmgAmount;
        }
        if (CurrentHealth < 0)
            CurrentHealth = 0;
    }

    public void HealingValue(int healingAmount)
    {
        if (CurrentHealth < MaxHealth)
            CurrentHealth += healingAmount;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;  
    }
}
