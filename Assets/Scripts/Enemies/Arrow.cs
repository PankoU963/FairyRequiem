using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float timerUntilDespawn;
    private float timer;
    public int damageAmount;

    private void Start()
    {
        timer = timerUntilDespawn;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Enemy"))
        {
            if (other.transform.CompareTag("Player"))
            {
                IDamageable damageable = other.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}
