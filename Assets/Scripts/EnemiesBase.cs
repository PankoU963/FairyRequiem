using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemiesBase : MonoBehaviour
{
    private enum EnemyType {Melee, Range}

    private NavMeshAgent enemyAgent;
    private Animator animator;
    [SerializeField] private Transform currentTarget;

    [SerializeField] EnemyType enemyType;
    [SerializeField] private ShootArrowEnemies shootArrow;

    [SerializeField] private float AttackDistance;
    [SerializeField] private float moveDistance;
    [SerializeField] private bool isAttacking;
    private GameObject player;

    private Health health;
    [SerializeField] private Image fillHealthBarImage;
    [SerializeField] private Transform healthBarLookAtCamera;
    [SerializeField] private Transform healthBarCanvas;

    public bool hurt;

    void Start()
    {
        health = GetComponent<Health>();
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        enemyAgent.avoidancePriority = Random.Range(30, 60);
        enemyAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        player = GameObject.FindGameObjectWithTag("Player");

        if(enemyType == EnemyType.Range)
        {
            shootArrow = GetComponent<ShootArrowEnemies>();
        }

        healthBarLookAtCamera = Camera.main.transform; //get the camera with the tag "MainCamera"

        health.CurrentHealth = health.MaxHealth;
        fillHealthBarImage = transform //get the fill from the healthbar
            .Find("EnemyCanvas/HealthBar_Frame/HealthBar_bg/HealthBar_Fill")
            ?.GetComponent<Image>();

        health.OnHealthChanged += UpdateHealth;
    }

    void Update()
    {
        Attack();
    }
    private void LateUpdate()
    {
        if (healthBarCanvas != null && healthBarLookAtCamera != null)
        {

            // Opción 2 (más simple): copia la rotación de la cámara (solo Y si quieres)
            healthBarCanvas.forward = healthBarLookAtCamera.forward;
        }
    }

    private void Attack()
    {
        currentTarget = player.transform;

        moveDistance = Vector3.Distance(transform.position, currentTarget.position);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        SmoothLookAt(currentTarget);
        if (!hurt)
        {
            if (enemyType == EnemyType.Melee)
            {
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
                    SmoothLookAt(currentTarget);
                    if (!isAttacking)
                    {
                        enemyAgent.isStopped = false;
                        enemyAgent.SetDestination(currentTarget.position);
                        animator.SetFloat("Move", 1);
                        animator.SetBool("Attack", false);
                    }
                }
            }
            if (enemyType == EnemyType.Range)
            {
                if (isAttacking)
                {
                    enemyAgent.isStopped = true;
                    if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
                    {
                        isAttacking = false;
                        animator.SetBool("Attack", false);
                    }
                }

                if (!isAttacking && !   stateInfo.IsName("Attack"))
                {
                    isAttacking = true;
                    enemyAgent.ResetPath();
                    enemyAgent.isStopped = true;
                    enemyAgent.velocity = Vector3.zero;
                    animator.SetBool("Attack", true);
                    animator.SetFloat("Move", 0);
                    SmoothLookAt(currentTarget);
                    shootArrow.ShootArrow();
                }

            }
        }
        if(stateInfo.IsName("Hurt") && stateInfo.normalizedTime >= 0.95f)
        {
            hurt = false;
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

    public void UpdateHealth(int current, int max)
    {
        float percent = Mathf.Clamp01((float)current / max);
        fillHealthBarImage.fillAmount = percent;
    }

    public void TakeDamage(int amount)
    {
        hurt = true;
        animator.SetTrigger("Hurt");
        animator.SetBool("Attack", false);
        isAttacking = false;

        health.TakeDamage(amount);
        UpdateHealth(health.CurrentHealth, health.MaxHealth);
    }

}
