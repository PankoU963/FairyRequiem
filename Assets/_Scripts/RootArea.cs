using UnityEngine;

public class RootArea : MonoBehaviour
{
    private float activeTime = 1f;
    public int damageAmount;
    void Start()
    {
        Destroy(gameObject, activeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            EnemiesBase enemiesBase = other.GetComponent<EnemiesBase>();
            if (enemiesBase != null)
            {
                enemiesBase.TakeDamage(damageAmount);
            }
        }
    }
}
