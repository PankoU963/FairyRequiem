using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [SerializeField] public enum BossStage { Fall, Idle, Attack, Scare, FallEnd, Dead}
    [SerializeField] public BossStage stage = BossStage.Fall;
    [SerializeField] private int vida = 5;
    [SerializeField] private float attackInterval = 5f;
    [SerializeField] private float attackTimer = 0f;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] Transform enemiesSpawn;
    private bool isSpawn = false;
    private Animator animator;
    private Rigidbody rb;
    public GameObject tronco;
    private Log logScript;
    private float lastLogDurability = 1;
    private bool isJumpingByDamage = false;

    private EndGame endgame;

    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        tronco = GameObject.FindGameObjectWithTag("LogBoss");
        enemiesSpawn = GameObject.FindGameObjectWithTag("EnemieSpawnBoss").transform;
        stage = BossStage.Fall;

        logScript = tronco.GetComponentInParent<Log>();
        lastLogDurability = logScript.CurrentDurability;

        endgame = GameObject.FindGameObjectWithTag("EndGame").GetComponent<EndGame>();
    }

    void Update()
    {
        if (vida > 1) transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        else if (vida <= 1) transform.LookAt(transform.position + Vector3.left);
        
        if (vida <= 0)
        {
            stage = BossStage.Dead;
            endgame.bossDead = true;
            return;
        }

        if (logScript.CurrentDurability != lastLogDurability)
        {
            lastLogDurability = logScript.CurrentDurability;
            RecibirDanio(1);
            stage = BossStage.Fall;
        }

        switch (stage)
        {
            case BossStage.Fall:
                if (isJumpingByDamage)
                {
                    // No sobrescribas la fuerza del salto por daño
                    break;
                }
                if (vida > 1)
                {
                    Vector3 direction = (tronco.transform.position - transform.position).normalized;
                    rb.isKinematic = false;
                    rb.linearVelocity = direction * 20f;
                    if (Vector3.Distance(transform.position, tronco.transform.position) <= 0.5f)
                    {
                        rb.isKinematic = false;
                        rb.useGravity = true;
                        rb.linearVelocity = Vector3.zero;
                        StartCoroutine(EnterFallEndThenIdle());
                    }
                    break;
                }
                else
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.AddForce(Vector3.down);
                    StartCoroutine(EnterFallEndThenIdle());
                    break;
                }
            case BossStage.Idle:
                rb.isKinematic = true;
                rb.useGravity = false;
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackInterval)
                {
                    attackTimer = 0f;
                    stage = BossStage.Attack;
                }
                break;
            case BossStage.Attack:

                if (!isSpawn)
                {
                    int numberEnemies = Random.Range(1, 4);
                    for(int i = 0; i < numberEnemies; i++)
                    {
                        Instantiate(enemies[Random.Range(0, 3)], enemiesSpawn.position, Quaternion.identity);
                    }

                    isSpawn = true;
                }
                StartCoroutine(EntertoIdle());
                break;
            case BossStage.Scare:
                break;
            case BossStage.FallEnd:
                // Espera 1 segundo antes de pasar a Idle
                // El cambio de estado se gestiona en la corrutina EnterFallEndThenIdle
                break;
        }
    }

    private IEnumerator EnterFallEndThenIdle()
    {
        stage = BossStage.FallEnd;
        yield return new WaitForSeconds(1f);
        attackTimer = 0f;
        if (vida <= 1)
        {
            stage = BossStage.Scare;
            yield break; // Sale de la corrutina si el boss está derrotado
        }
        stage = BossStage.Idle;
    }
    private IEnumerator EntertoIdle()
    {
        yield return new WaitForSeconds(3f);
        stage = BossStage.Idle;
        attackTimer = 0f;
        isSpawn = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((stage == BossStage.Scare || stage == BossStage.Attack) && other.gameObject.CompareTag("Weapon"))
        {
            RecibirDanio(1);
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vida -= cantidad;
        if (vida > 0)
        {
            stage = BossStage.Fall;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.up * 7.5f, ForceMode.Impulse); // Simula un golpe
            isJumpingByDamage = true;
            StartCoroutine(ResetJumpByDamage());
        }
        else if (vida <= 0)
        {
            stage = BossStage.Fall;
            Invoke(nameof(EntrarScare), 1f); // Espera 1 segundo antes de scare
        }
    }

    private IEnumerator ResetJumpByDamage()
    {
        yield return new WaitForSeconds(0.3f); // Tiempo suficiente para el salto
        isJumpingByDamage = false;
    }

    private void EntrarScare()
    {
        stage = BossStage.Scare;
    }
}
