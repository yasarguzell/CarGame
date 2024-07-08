public interface IHealth
{
    float GetCurrentHealth();
    void TakeDamage(int amount);
    void Die();
    void CheckHealth();
}