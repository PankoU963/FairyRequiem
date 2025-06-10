using UnityEngine;
using UnityEngine.AI;

public class EnemiesBase : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Animator animator;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float AttackDistance;
    [SerializeField] private float moveDistance;
    [SerializeField] private bool isAttacking;
    private GameObject player;

    void Start()
    {

        enemyAgent = GetComponent<NavMeshAgent>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        enemyAgent.avoidancePriority = Random.Range(30, 60);
        enemyAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        currentTarget = player.transform;

        moveDistance = Vector3.Distance(transform.position, currentTarget.position);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isAttacking)
        {
            enemyAgent.isStopped = true;
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
            {
                isAttacking = false;
                animator.SetBool("Attack", false);
            }
        }

        if (moveDistance <= AttackDistance)
        {
            if (!isAttacking && stateInfo.IsName("Attack") == false)
            {
                isAttacking = true;
                enemyAgent.ResetPath();
                enemyAgent.isStopped = true;
                enemyAgent.velocity = Vector3.zero;
                animator.SetBool("Attack", true);
                animator.SetFloat("Move", 0);
                SmoothLookAt(currentTarget);
            }
        }
        else
        {
            if (!isAttacking)
            {
                enemyAgent.isStopped = false;
                enemyAgent.SetDestination(currentTarget.position);
                animator.SetFloat("Move", 1);
                animator.SetBool("Attack", false);
                SmoothLookAt(currentTarget);
            }
        }
    }
    private void SmoothLookAt(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
