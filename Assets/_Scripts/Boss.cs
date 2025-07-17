using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [SerializeField] public enum BossStage { Fall, Idle, Attack, Scare, FallEnd }
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
    }

    void Update()
    {
        if (vida > 1) transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        else if (vida <= 1) transform.LookAt(transform.position + Vector3.left);

        if (logScript.CurrentDurability != lastLogDurability)
        {
            lastLogDurability = logScript.CurrentDurability;
            RecibirDanio(1);
            stage = BossStage. Fall;
        }

        switch (stage)
        {
            case BossStage.Fall:
                if (vida > 1)
                {
                    Vector3 direction = (tronco.transform.position - transform.position).normalized;

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
                    Debug.Log("Boss is attacking!");
                    stage = BossStage.Attack;
                }
                break;
            case BossStage.Attack:

                if (!isSpawn)
                {
                    Instantiate(enemies[Random.Range(0, 3)], enemiesSpawn.position, Quaternion.identity);
                    isSpawn = true;
                }
                StartCoroutine(EntertoIdle());
                break;
            case BossStage.Scare:
                Debug.Log("Boss is scared!");
                gameObject.SetActive(false); // Desactiva el boss o realiza otra acción
                // Animación de miedo o derrota
                break;
            case BossStage.FallEnd:
                // Espera 1 segundo antes de pasar a Idle
                // El cambio de estado se gestiona en la corrutina EnterFallEndThenIdle
                break;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (stage == BossStage.Fall && collision.gameObject.CompareTag("Ground"))
    //    {
    //        StartCoroutine(EnterFallEndThenIdle());
    //    }
    //}

    private IEnumerator EnterFallEndThenIdle()
    {
        stage = BossStage.FallEnd;
        yield return new WaitForSeconds(1f);
        stage = BossStage.Idle;
        attackTimer = 0f;
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
        if ((stage == BossStage.Idle || stage == BossStage.Attack) && other.gameObject.CompareTag("Weapon"))
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
            rb.AddForce(Vector3.up * 15f, ForceMode.Impulse); // Simula un golpe
        }
        else if (vida <= 0)
        {
            stage = BossStage.Fall;
            Invoke(nameof(EntrarScare), 1f); // Espera 1 segundo antes de scare
        }
        else
        {
            Debug.Log("Error: Vida no puede ser negativa");
            Destroy(gameObject); // Destruye el boss al ser derrotado
        }
    }

    private void EntrarScare()
    {
        stage = BossStage.Scare;
    }
}
