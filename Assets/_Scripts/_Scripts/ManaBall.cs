using UnityEngine;

public class ManaBall : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int amount;
 
    private Rigidbody rb;
    private GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Mana>().CurrentMana += amount;
            Destroy(gameObject);
        }

    }
}
