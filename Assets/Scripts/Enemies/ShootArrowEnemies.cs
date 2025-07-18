using UnityEngine;

public class ShootArrowEnemies : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int damageAmount = 10;
    private Transform playerTransform;
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float arcHeight = 5f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        arrow.GetComponent<Arrow>().damageAmount = damageAmount;

        Vector3 directionXZ = playerTransform.position - shootPoint.position;
        directionXZ.y = 0;
        directionXZ.Normalize();

        Vector3 launchDirection = directionXZ + Vector3.up * arcHeight;
        launchDirection.Normalize();

        rb.useGravity = true;

        rb.linearVelocity = launchDirection * initialSpeed;

        arrow.transform.forward = launchDirection;
    }
}
