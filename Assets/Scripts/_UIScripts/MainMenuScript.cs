using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // Asigna tu imagen negra en el inspector
    [SerializeField] private AudioSource musicSource; // Asigna tu AudioSource en el inspector

    public void PlayGame()
    {
        int timeToWait = 3;
        StartCoroutine(FadeAndLoad(timeToWait));
    }

    private System.Collections.IEnumerator FadeAndLoad(int seconds)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;
        float initialVolume = musicSource != null ? musicSource.volume : 0.5f;

        while (elapsed < seconds)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / seconds);

            // Fade visual
            color.a = t;
            fadeImage.color = color;

            // Fade audio
            if (musicSource != null)
                musicSource.volume = Mathf.Lerp(initialVolume, 0f, t);

            yield return null;
        }
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
