using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Load the game
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

}
