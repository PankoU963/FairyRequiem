using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // Transform del jugador
    [SerializeField] private Vector3 offset; // Offset de la cámara respecto al jugador
    [SerializeField] private float smoothTime = 0.5f; // Tiempo para alcanzar el objetivo (ajusta para más o menos inercia)

    private Vector3 velocity = Vector3.zero; // Velocidad actual de la cámara (usada por SmoothDamp)
    private bool enZonaFija = false;
    private Transform zonaFijaTransform;

    private void Start()
    {
        player ??= GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        Vector3 targetPosition;
        if (enZonaFija && zonaFijaTransform != null)
        {
            // La cámara se mueve hacia la posición de la zona fija SOLO en X
            targetPosition = new Vector3(zonaFijaTransform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            // Sigue al jugador solo en X
            targetPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void ActivarZonaFija(Transform zona)
    {
        enZonaFija = true;
        zonaFijaTransform = zona;
    }

    public void DesactivarZonaFija()
    {
        enZonaFija = false;
        zonaFijaTransform = null;
    }
}