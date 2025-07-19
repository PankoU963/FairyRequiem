using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private int maxMana = 90;
    [SerializeField] private int currentMana;

    public int MaxMana { get => maxMana; set => maxMana = value; }
    public int CurrentMana { get => currentMana; set => currentMana = Mathf.Clamp(value, 0, maxMana); }
    private void Start()
    {
        CurrentMana = MaxMana;
    }
}
