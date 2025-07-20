using UnityEngine;

public class ShootArrowEnemies : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int damageAmount = 10;
    private Transform playerTransform;
    [SerializeField] private float initialSpeed = 10f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        arrow.GetComponent<Arrow>().damageAmount = damageAmount;

        Vector3 direction = (playerTransform.position - shootPoint.position).normalized;

        rb.useGravity = true;
        rb.linearVelocity = direction * initialSpeed;

        arrow.transform.forward = new Vector3(direction.x, 0, direction.z).normalized;
    }
}
