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
        if(!other.CompareTag("Player"))
        {
            EnemiesBase enemiesBase = other.GetComponent<EnemiesBase>();
            if (enemiesBase != null)
            {
                enemiesBase.TakeDamage(damageAmount);
            }
        }
    }
}
