using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Mana playerMana;
    void Start()
    {
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<Mana>();
    }

    void Update()
    {
        slider.value = (float)playerMana.CurrentMana / playerMana.MaxMana;
    }
}