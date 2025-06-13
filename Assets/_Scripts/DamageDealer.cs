using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
            // Si es un proyectil, puedes destruirlo aquí:
            // Destroy(gameObject);
        }
    }
}
