using UnityEngine;

public class Flower : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SoundManager.Playsound(SoundType.FLOR);
        Destroy(gameObject);
    }
}
