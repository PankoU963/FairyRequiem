using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossStage { Fall, Idle, Attack, Scare }
    [SerializeField] public BossStage stage = BossStage.Fall;
    [SerializeField] private int vida = 3;
    [SerializeField] private float attackInterval = 10f;
    private float attackTimer = 0f;

    private Rigidbody rb;
    public GameObject tronco;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stage = BossStage.Fall;
    }

    void Update()
    {
        switch (stage)
        {
            case BossStage.Fall:
                // Espera a tocar el suelo
                break;
            case BossStage.Idle:
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackInterval)
                {
                    stage = BossStage.Attack;
                    attackTimer = 0f;
                }
                break;
            case BossStage.Attack:
                // Aquí va la lógica de ataque
                Debug.Log("Boss is attacking!");
                stage = BossStage.Idle;
                break;
            case BossStage.Scare:
                Debug.Log("Boss is scared!");
                gameObject.SetActive(false); // Desactiva el boss o realiza otra acción
                // Animación de miedo o derrota
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stage == BossStage.Fall && collision.gameObject.CompareTag("Ground"))
        {
            stage = BossStage.Idle;
            attackTimer = 0f;
        }
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
