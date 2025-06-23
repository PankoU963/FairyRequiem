using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public EnemySpawnData enemysToSpawn;

    public Transform[] spawnPoints;

    [SerializeField] GameObject wallF, WallB;

    private bool trigger;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wallF.gameObject.SetActive(true);
            WallB.gameObject.SetActive(true);
            SpawnEnemies();
            Destroy(gameObject);
        }
    }

    void SpawnEnemies()
    {
        Debug.Log("spawneo de enemigos");
        for (int i = 0; i < enemysToSpawn.enemyPrefab.Count; i++)
        {
            for (int j = 0; j < enemysToSpawn.amount[i]; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Debug.Log("enemigo spawneado"+j);
                Instantiate(enemysToSpawn.enemyPrefab[i], spawnPoint.position,spawnPoint.rotation);
            }
        }
    }


}
