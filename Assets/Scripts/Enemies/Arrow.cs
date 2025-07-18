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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
        {
            if (collision.transform.CompareTag("Player"))
            {
                IDamageable damageable = collision.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                }
            }
            Destroy(gameObject);
        }

    }
}
