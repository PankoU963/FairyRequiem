using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    [SerializeField] private Animator attackAnimator;
    private AnimatorStateInfo stateInfo;
    private bool attacked;

    private void Update()
    {
        stateInfo = attackAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.95f)
        {
            attacked = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null && !attacked)
            {
                damageable.TakeDamage(damageAmount);
                attacked = true;
            }
        }
    }
}
