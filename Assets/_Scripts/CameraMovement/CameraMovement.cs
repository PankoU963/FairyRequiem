using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Transform del jugador
    public Vector3 offset; // Offset de la cámara respecto al jugador
    public float smoothTime = 0.3f; // Tiempo para alcanzar el objetivo (ajusta para más o menos inercia)

    private Vector3 velocity = Vector3.zero; // Velocidad actual de la cámara (usada por SmoothDamp)

    void LateUpdate()
    {
        // Solo sigue el eje X del jugador, mantiene Y y Z de la cámara
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}