using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public bool bossDead;
    [SerializeField] private Graphic blackScreen;

    [SerializeField] private float duration = 2f;

    [SerializeField] private float timer = 0f;

    void Awake()
    {
        bossDead = false;
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

            }
        }
    }
}
