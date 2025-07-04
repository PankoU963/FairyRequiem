using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public EnemySpawnData enemysToSpawn;
    public Transform[] spawnPoints;

    [SerializeField] private GameObject wallF, WallB;

    [SerializeField] GameObject zonaFija;

    private int currentWave = 0;
    private bool isSpawning = false;
    private bool activated = false;

    void Update()
    {
        if (activated && !isSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            currentWave++;

            if (currentWave <= 3)
            {
                StartCoroutine(SpawnWave(currentWave));
            }
            else
            {
                Debug.Log("mataste a todos, chimba eso");
                wallF.SetActive(false);
                WallB.SetActive(false);
                enabled = false; // Desactiva este script
            }

            if (currentWave >= 3)
            {
                Destroy(zonaFija);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("si choco este hptsdadsadsadadsadadsdagdsgaghfdhsa");
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            wallF.SetActive(true);
            WallB.SetActive(true);

            currentWave = 1;

            if (currentWave <= 3)
            {
                StartCoroutine(SpawnWave(currentWave));
            }
            
                
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        isSpawning = true;

        Debug.Log("2 segundos para la ola: " + waveNumber);
        yield return new WaitForSeconds(2f);

        Debug.Log("oleada  " + waveNumber);

        if (waveNumber == 1)
        {
            SpawnFromList(enemysToSpawn.enemyPrefab1, enemysToSpawn.amount1);
        }
        else if (waveNumber == 2)
        {
            SpawnFromList(enemysToSpawn.enemyPrefab2, enemysToSpawn.amount2);
        }
        else if (waveNumber == 3)
        {
            SpawnFromList(enemysToSpawn.enemyPrefab3, enemysToSpawn.amount3);
        }
        
        isSpawning = false;
    }

    void SpawnFromList(System.Collections.Generic.List<GameObject> prefabs, System.Collections.Generic.List<int> amounts)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            for (int j = 0; j < amounts[i]; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(prefabs[i], spawnPoint.position, spawnPoint.rotation);
            }
        }

    }
}
