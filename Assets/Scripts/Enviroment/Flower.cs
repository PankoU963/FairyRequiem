using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    void OnTriggerEnter(Collider other)
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;

        SoundManager.Playsound(SoundType.FLOR);
        Destroy(gameObject);
    }
}
