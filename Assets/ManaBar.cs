using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Slider slider;
    private Mana playerMana;
    void Start()
    {
        slider = transform.GetChild(1).GetChild(1).GetComponent<Slider>();
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Mana>();
    }
    void Update()
    {
        slider.value = (float)playerMana.CurrentMana / playerMana.MaxMana;
    }
}
