using UnityEngine;

public class ZonaFija : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMov;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraMov.ActivarZonaFija(transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraMov.ActivarZonaFija(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraMov.DesactivarZonaFija();
        }
    }
}
