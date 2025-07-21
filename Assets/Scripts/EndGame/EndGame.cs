using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public bool bossDead;
    [SerializeField] private Graphic blackScreen;

    [SerializeField] private float duration = 2f;

    [SerializeField] private float timer = 0f;

    [SerializeField] private Heal checkEnd;

    void Awake()
    {
        bossDead = false;
        checkEnd = GameObject.FindGameObjectWithTag("Player").GetComponent<Heal>();
    }

    void Update()
    {
        if (bossDead)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / duration);

            Color c = blackScreen.color;
            c.a = alpha;
            blackScreen.color = c;

            if (alpha >= 1f)
            {
                if(checkEnd.Uses >= 1)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    SceneManager.LoadScene(4);
                }
            }
        }
    }
}
