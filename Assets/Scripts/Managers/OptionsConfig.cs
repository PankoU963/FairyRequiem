using UnityEngine;
using UnityEngine.UI;

public class OptionsConfig : MonoBehaviour
{
    [SerializeField] private Slider sounds;
    [SerializeField] private Toggle mute;
    [SerializeField] private Toggle parallaxBackground;

    void Start()
    {
        sounds.value = PlayerPrefs.GetFloat("Volume", 1f);
        if(PlayerPrefs.GetInt("Mute") == 0)
        {
            mute.isOn = false;
        }
        if(PlayerPrefs.GetInt("Mute") == 1)
        {
            mute.isOn = true;
        }
        if (PlayerPrefs.GetInt("Parallax") == 0)
        {
            parallaxBackground.isOn = false;
        }
        if (PlayerPrefs.GetInt("Parallax") == 1)
        {
            parallaxBackground.isOn = true;
        }
    }
    public void ApplyConfig()
    {
        if (mute.isOn)
        {
            SoundManager.SetVolume(0f);
        }
        else
        {
            SoundManager.SetVolume(sounds.value);
        }
        if (parallaxBackground.isOn)
        {
            PlayerPrefs.SetInt("Parallax", 1);
        } else
        {
            PlayerPrefs.SetInt("Parallax", 0);
        }

        Debug.Log(PlayerPrefs.GetInt("Parallax"));
        Debug.Log(PlayerPrefs.GetInt("Mute"));
        Debug.Log(PlayerPrefs.GetFloat("Volume"));
    }
}
