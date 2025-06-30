using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public EnemySpawnData enemysToSpawn;

    public Transform[] spawnPoints;

    [SerializeField] GameObject wallF, WallB;

    private bool trigger;

    private int enemyCount;

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wallF.gameObject.SetActive(true);
            WallB.gameObject.SetActive(true);
            // SpawnEnemies();
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnEnemies()
    {
        Debug.Log("spawneo de enemigos");
        if (enemyCount == 0)
        {
            wave1();
            yield return new WaitForSeconds(5f);
        }
        
    }

    void wave1()
    {
        for (int i = 0; i < enemysToSpawn.enemyPrefab1.Count; i++)
        {
            for (int j = 0; j < enemysToSpawn.amount1[i]; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Debug.Log("enemigo spawneado" + j);
                Instantiate(enemysToSpawn.enemyPrefab1[i], spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    void wave2()
    {
        for (int i = 0; i < enemysToSpawn.enemyPrefab2.Count; i++)
        {
            for (int j = 0; j < enemysToSpawn.amount2[i]; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Debug.Log("enemigo spawneado" + j);
                Instantiate(enemysToSpawn.enemyPrefab1[i], spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    void wave3()
    {
        for (int i = 0; i < enemysToSpawn.enemyPrefab3.Count; i++)
        {
            for (int j = 0; j < enemysToSpawn.amount3[i]; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Debug.Log("enemigo spawneado" + j);
                Instantiate(enemysToSpawn.enemyPrefab1[i], spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
