using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    

    public delegate void HealthChanged(int current, int max);
    public event HealthChanged OnHealthChanged;

    public delegate void Death();
    public event Death OnDeath;


    //Accesors
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    //Functions

    void Awake()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int amount)
    {
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
        CurrentHealth = CurrentHealth > MaxHealth ? maxHealth : CurrentHealth; //Verify if it's overhealing
        Debug.Log($"{gameObject.name} ha recibido {amount} puntos de curación. Salud actual: {CurrentHealth}/{MaxHealth}");
    }

    private void Die() 
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
