using UnityEngine;

public class BossAnimatorControler : MonoBehaviour
{
    private Boss boss;
    private Animator animator;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (boss == null || animator == null) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Transitioning")) return;

        switch (boss.stage)
        {
            case Boss.BossStage.Fall:
                animator.ResetTrigger("Attack");
                animator.SetTrigger("IsFalling");
                break;
            case Boss.BossStage.Idle:
                animator.SetTrigger("IsIdle");
                break;
            case Boss.BossStage.Attack:
                if (boss.vida > 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    animator.SetTrigger("Attack");
                }
                break;
            case Boss.BossStage.Scare:
                animator.SetTrigger("IsScared");
                break;
            case Boss.BossStage.FallEnd:
                animator.SetTrigger("IsFallEnding");
                break;
            case Boss.BossStage.Dead:
                animator.SetTrigger("Dead");
                break;
        }
    }

    public void OnAttackAnimationEnd(int stateInfo)
    {
        if (stateInfo == 0)
        {
            return;
        }
        boss.stage = Boss.BossStage.Idle;
    }
}
