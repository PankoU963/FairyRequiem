using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int wave;
    public int numEnemies;

    [SerializeField] Transform[] enemySpawn;

    [SerializeField] GameObject[] enemyPrefab;

    [SerializeField] GameObject wallF, WallB;

    private GameObject player;

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
        }
    }

    private void EnemySpawn()
    {
        Random.Range(0, enemyPrefab.Length);
        
    }
}
