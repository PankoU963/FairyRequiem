using UnityEngine;

public class Trap : MonoBehaviour
{

    public GameObject trunkSpawnPoint;

    public GameObject trunkPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Instantiate(trunkPrefab, trunkSpawnPoint.transform.position, trunkSpawnPoint.transform.rotation);
        }
        
    }
}
