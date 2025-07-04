using UnityEngine;

public class ObjetoConsumible : MonoBehaviour
{
    public float floatSpeed = 1f;      // Velocidad del movimiento vertical
    public float floatAmount = 0.5f;   // Altura de oscilación
    public float rotationSpeed = 50f;  // Velocidad de rotación
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
