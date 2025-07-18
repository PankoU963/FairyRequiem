using UnityEngine;

public class Log : MonoBehaviour, IDestroyable
{
    [Header("Log Durability Settings")]
    [SerializeField] private float maxDurability = 160f;
    [SerializeField] private float currentDurability;
    [SerializeField] private GameObject[] stages; // 3 estados: 0 = entero, 1 = danado, 2 = casi destruido
    private ParticleSystem particles;
    
    public float MaxDurability
    {
        get => maxDurability;
        set
        {
            maxDurability = Mathf.Max(1f, value);
            currentDurability = Mathf.Clamp(currentDurability, 0f, maxDurability);
            UpdateVisualState();
        }
    }

    public float CurrentDurability
    {
        get => currentDurability;
        set
        {
            currentDurability = Mathf.Clamp(value, 0f, MaxDurability);
            UpdateVisualState();
        }
    }

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        CurrentDurability = MaxDurability;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        CurrentDurability -= amount;
        particles.Play();
    }

    private void UpdateVisualState()
    {
        float percentage = MaxDurability > 0f ? currentDurability / MaxDurability : 0f;

        if (stages.Length >= 3)
        {
            stages[0].SetActive(percentage > 0.66f);   // Etapa 1: entero
            stages[1].SetActive(percentage <= 0.66f && percentage > 0.33f); // Etapa 2: danado
            stages[2].SetActive(percentage <= 0.33f && percentage > 0f);    // Etapa 3: muy danado
        }

        if (percentage <= 0f)
        {
            Destroy(gameObject);
        }
    }
}