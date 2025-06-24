using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public EnemySpawnData enemysToSpawn;

    public Transform[] spawnPoints;

    [SerializeField] GameObject wallF, WallB;

    private bool trigger;

    private CameraMovement cameraMain;

    private Transform zone;

    [SerializeField] private List<GameObject> enemiesRemaining = new List<GameObject>();
    private void Start()
    {
        cameraMain = Camera.main.GetComponent<CameraMovement>();
        zone = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            for (int i = 0; i < enemiesRemaining.Count; i++)
            {
                if (enemiesRemaining[i] == null)
                {
                    enemiesRemaining.RemoveAt(i);
                }
            }
                if (enemiesRemaining.Count == 0)
            {
                cameraMain.DesactivarZonaFija();
                Destroy(zone.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trigger) return;

            cameraMain.ActivarZonaFija(zone);
            wallF.gameObject.SetActive(true);
            WallB.gameObject.SetActive(true);
            SpawnEnemies();
            trigger = true;
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
                GameObject enemy = Instantiate(enemysToSpawn.enemyPrefab[i], spawnPoint.position,spawnPoint.rotation);
                enemiesRemaining.Add(enemy);
            }
        }
    }


}
