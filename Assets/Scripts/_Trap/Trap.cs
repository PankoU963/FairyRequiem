using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Trunk Trap Settings")]
    [SerializeField] private GameObject trunkSpawnPoint;  // El lugar donde se generará el tronco
    [SerializeField] private GameObject trunkPrefab;      // El prefab del tronco

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")             // Si el objeto que entra en el trigger es el jugador
        {
            Instantiate(trunkPrefab, trunkSpawnPoint.transform.position, trunkSpawnPoint.transform.rotation);
        }                                                 // Instancia el tronco en el punto de generación
    }
}
