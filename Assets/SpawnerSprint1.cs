using UnityEngine;

public class SpawnerSprint1 : MonoBehaviour
{
    public GameObject enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            GameObject enemies = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
            enemies.transform.SetParent(transform);
        }
    }
}
