using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    public delegate void HealthChanged(int current, int max);
    public event HealthChanged OnHealthChanged;

    public delegate void Death();
    public event Death OnDeath;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    void Awake()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (gameObject.tag == "Enemy")
        {
            SoundManager.Playsound(SoundType.DAÑO_ENEMIGO);
        }
        else
        {
            SoundManager.Playsound(SoundType.DAÑO);
        }
        
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        Debug.Log($"{gameObject.name} ha recibido {amount} puntos de daño. Salud actual: {CurrentHealth}/{MaxHealth}");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        CurrentHealth += amount;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
