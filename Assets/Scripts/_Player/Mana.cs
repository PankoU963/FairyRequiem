using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] public int maxMana = 90;
    [SerializeField] public int currentMana;

    public int MaxMana { get => maxMana; set => maxMana = value; }
    public int CurrentMana { get => currentMana; set => currentMana = Mathf.Clamp(value, 0, maxMana); }
    private void Start()
    {
        CurrentMana = MaxMana;
    }
}
