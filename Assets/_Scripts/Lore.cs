using UnityEngine;
using UnityEngine.SceneManagement;

public class Lore : MonoBehaviour
{
    [SerializeField] private GameObject[] images;
    [SerializeField] private int index;

    void Start()
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }
        index = 0;
        if (images.Length > 0)
        {
            images[index].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CambiarImagen();
        }
    }

    public void CambiarImagen()
    {
        if (index < images.Length - 1)
        {
            images[index].SetActive(false);

            index++;

            images[index].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
