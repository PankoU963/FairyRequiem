using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    public bool dead;

    [SerializeField] private Graphic blackScreen;

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject button;

    [SerializeField] private float duration = 2f;

    [SerializeField] private float timer = 0f;


    void Awake()
    {
        dead = false;
    }
    private void Start()
    {
        blackScreen.gameObject.SetActive(false);
        text.SetActive(false);
        button.SetActive(false);
    }
    void Update()
    {
        if (dead)
        {
            blackScreen.gameObject.SetActive(true);
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / duration);

            Color c = blackScreen.color;
            c.a = alpha;
            blackScreen.color = c;

            if (alpha >= 1f)
            {
                text.SetActive(true);
                button.SetActive(true);
                Cursor.visible = true;
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(2);
    }
}
