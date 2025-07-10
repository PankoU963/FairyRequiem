using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Health playerHealth;
    void Start()
    {
        slider = transform.GetChild(1).GetComponent<Slider>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}
