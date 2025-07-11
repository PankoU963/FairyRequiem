using UnityEngine;

public class BossAnimatorControler : MonoBehaviour
{
    private Boss boss;
    private Animator animator;

    void Start()
    {
        boss = GetComponent<Boss>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (boss == null || animator == null) return;

        switch (boss.stage)
        {
            case Boss.BossStage.Fall:
                animator.Play("Boss_Fall");
                break;
            case Boss.BossStage.Idle:
                animator.Play("Boss_Idle");
                break;
            case Boss.BossStage.Attack:
                animator.Play("Boss_Attack");
                break;
            case Boss.BossStage.Scare:
                animator.Play("Boss_Scare");
                break;
        }
    }
}
