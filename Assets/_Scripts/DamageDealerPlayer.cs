using UnityEngine;

public class DamageDealerPlayer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;

    [SerializeField] private Animator attackAnimator;
    [SerializeField] private ComboSystem comboSystem;
    private AnimatorStateInfo stateInfo;
    private bool canAttack;

    private void Update()
    {
        stateInfo = attackAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Movement"))
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player") && canAttack)
        {
            EnemiesBase enemiesBase = other.GetComponent<EnemiesBase>();
            if (enemiesBase != null)
            {
                enemiesBase.TakeDamage(damageAmount);
            }
        }
    }
}
