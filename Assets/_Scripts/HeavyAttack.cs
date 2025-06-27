using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class HeavyAttack : MonoBehaviour
{
    public float maxChargeTime = 3f;
    public float minChargeTime = 0.3f;
    public float baseDamage = 10f;
    public float maxExtraDamage = 20f;

    private float currentCharge = 0f;
    private bool isCharging = false;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Attack.started += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
                StartCharging();
        };

        inputActions.Player.Attack.canceled += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
                ReleaseAttack();
        };

        inputActions.Player.Attack.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction)
                DoBasicAttack();
        };
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        if (isCharging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeTime);
        }
    }

    private void StartCharging()
    {
        isCharging = true;
        currentCharge = 0f;
        Debug.Log("Comenzó a cargar el ataque pesado");
    }

    private void ReleaseAttack()
    {
        isCharging = false;
        float ratio = Mathf.InverseLerp(minChargeTime, maxChargeTime, currentCharge);
        float totalDamage = baseDamage + ratio * maxExtraDamage;
        Debug.Log($"¡Ataque pesado ejecutado! Daño: {totalDamage:F1}");
        currentCharge = 0f;
    }

    private void DoBasicAttack()
    {
        Debug.Log("Ataque básico ejecutado");
        // Aquí puedes llamar a animaciones o lógica de daño simple
    }
}
