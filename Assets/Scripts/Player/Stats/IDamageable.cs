public interface IDamageable
{
    HealthSO Data { get; }
    void TakeDamage(int amount);
}