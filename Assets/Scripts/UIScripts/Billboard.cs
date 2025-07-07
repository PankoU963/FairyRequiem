using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Hacer que la barra mire hacia la cámara solo en el eje Z (como un juego 2D)
        Vector3 lookDirection = mainCamera.transform.forward;
        lookDirection.y = 0; // Evita que se incline
        transform.forward = lookDirection;
    }
}