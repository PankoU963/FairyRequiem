using UnityEngine;

public class DamageDealerPlayer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    [SerializeField] private Animator attackAnimator;
    private AnimatorStateInfo stateInfo;
    private bool attacked;

    private void Update()
    {
        //stateInfo = attackAnimator.GetCurrentAnimatorStateInfo(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && !attacked)
        {
            Debug.Log("Player");
            damageable.TakeDamage(damageAmount);
            attacked = true;
            // Si es un proyectil, puedes destruirlo aquí:
            // Destroy(gameObject);
        }
    }
}
