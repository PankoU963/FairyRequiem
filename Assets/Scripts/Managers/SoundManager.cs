using UnityEngine;

public enum SoundType
{
    PASOS,
    ATAQUE,
    DAÑO,
    FLOR,
    DAÑO_ENEMIGO
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceAmbiente;

    private float volume = 1f;
    private void Awake()
    {
        instance = this;

        volume = PlayerPrefs.GetFloat("Volume", 1f);

        if (audioSource != null)
            audioSource.volume = volume;

        if (audioSourceAmbiente != null)
            audioSourceAmbiente.volume = volume;
    }

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public static void Playsound(SoundType sound)
    {
        if (instance == null || instance.audioSource == null)
            return;

        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], instance.volume);
    }

    public static void SetVolume(float newVolume)
    {
        newVolume = Mathf.Clamp01(newVolume);
        PlayerPrefs.SetFloat("Volume", newVolume);
        PlayerPrefs.Save();

        instance.volume = newVolume;

        if (instance.audioSource != null)
            instance.audioSource.volume = newVolume;

        if (instance.audioSourceAmbiente != null)
            instance.audioSourceAmbiente.volume = newVolume;
    }
}
