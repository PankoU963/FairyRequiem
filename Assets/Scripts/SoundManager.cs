using UnityEngine;

public enum SoundType
{
    PASOS,
    ATAQUE,
    DAÑO,
    AMBIENTE_TRANSICION,
    FLOR,
    DAÑO_ENEMIGO
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void Playsound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

}
