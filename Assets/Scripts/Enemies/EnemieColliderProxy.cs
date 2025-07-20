using UnityEngine;

public class EnemieColliderProxy : MonoBehaviour
{
    [SerializeField] private Collider enemyCollider;

    public void EnemieColliderActivation(int isActive)
    {
        if (isActive == 1)
        {
            enemyCollider.enabled = true;
        }
        else
        {
            enemyCollider.enabled = false;
        }
    }
}
