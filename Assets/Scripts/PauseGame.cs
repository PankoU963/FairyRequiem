using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool gamePaused = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("noda");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }
}
