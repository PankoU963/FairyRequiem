using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject manaBall;

    private GameOver gameOver;
    void Awake()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        gameOver = GetComponent<GameOver>();
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
        OnDeath?.Invoke();
        if(transform.tag != "Player" && manaBall != null)
        {
            Instantiate(manaBall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (transform.tag == "Player")
        {
            if(gameOver != null)
            gameOver.dead = true;
        }


    }
}
