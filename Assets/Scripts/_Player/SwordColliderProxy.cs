using UnityEngine;

public class SwordColliderProxy : MonoBehaviour
{
    [SerializeField] private DamageDealerPlayer damageDealerPlayer;

    public void SwordColliderActivation(int isActive)
    {
        damageDealerPlayer?.SwordColliderActivation(isActive);
    }
}