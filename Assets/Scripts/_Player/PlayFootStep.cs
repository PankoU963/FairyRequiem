using Unity.VisualScripting;
using UnityEngine;

public class PlayFootStep : MonoBehaviour
{
    public void PlaySound()
    {
        SoundManager.Playsound(SoundType.PASOS);
    }
}
