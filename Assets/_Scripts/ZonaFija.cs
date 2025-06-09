using UnityEngine;

public class ZonaFija : MonoBehaviour
{
    public CameraMovement camara; // Asigna el script CameraMovement en el inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camara.ActivarZonaFija(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camara.DesactivarZonaFija();
        }
    }
}
