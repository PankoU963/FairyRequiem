using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    private Health health;
    [SerializeField] private int uses;
    [SerializeField] private int healAmount;
    [SerializeField] private TextMeshProUGUI usesText;

    private bool used;
    [SerializeField] private Image cdImage;
    [SerializeField] private float cdMax;
    [SerializeField] private float cdTimer;

    public int Uses { get => uses; set => uses = value; }

    void Start()
    {
        health = GetComponent<Health>();
        cdTimer = 0;
    }

    void Update()
    {
        usesText.text = Uses.ToString();
        cdImage.fillAmount = cdTimer / cdMax;
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            if(Uses <= 0) return;
            if(health.CurrentHealth == health.MaxHealth) return;
            if(used) return;
            UseFlower();
            Uses--;
            used = true;
        }
        CD();
    }

    private void UseFlower()
    {
        health.Heal(healAmount);
    }

    private void CD()
    {
        if(used)
        {
            cdTimer += Time.deltaTime;
        }
        if(cdTimer >= cdMax)
        {
            used = false;
            cdTimer = 0;
        }
    }
}
