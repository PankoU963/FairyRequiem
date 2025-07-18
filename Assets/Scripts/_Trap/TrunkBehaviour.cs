using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class TrunkBehaviour : MonoBehaviour
{
    [Header("Trunk Settings")]
    [SerializeField] private float rotationVelocity = 45f;
    [SerializeField] private float speed = 40f;
    [SerializeField] private float destroyTime = 10f; // Tiempo antes de destruir el tronco
    private float speedIn;
    private Vector3 velocidadRotacion;

    void Start()
    {
        speedIn = speed / 100f; // Convierte la velocidad a una escala adecuada para el movimiento
        velocidadRotacion = new Vector3(0f, -10 * rotationVelocity, 0f);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speedIn); // Mueve el tronco hacia adelante
        transform.Rotate(velocidadRotacion * Time.deltaTime); // Rota el tronco continuamente
        Destroy(gameObject, destroyTime); // Destruye el tronco después de X segundos
    }
}
