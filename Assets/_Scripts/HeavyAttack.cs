using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.Interactions;

public class HeavyAttack : MonoBehaviour
{
    [Header("Configuración de daño")]
    public float maxChargeTime = 3f;
    public float minChargeTime = 0.3f;
    public float baseDamage = 10f;
    public float maxExtraDamage = 20f;

    private float currentCharge = 0f;
    private bool isCharging = false;
    private bool isAttacking = false;
    private bool wasCharging = false;

    private InputSystem_Actions inputActions;

    [SerializeField] private Animator animator;
    private Movement movementScript;

    [Header("Barra de carga")]
    [SerializeField] private GameObject chargeBarPrefab;
    private GameObject chargeBarInstance;
    private Image chargeFillImage;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        movementScript = GetComponent<Movement>();

        inputActions.Player.Attack.started += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                wasCharging = true;
                StartCharging();
            }
        };

        inputActions.Player.Attack.canceled += ctx =>
        {
            if (wasCharging)
            {
                wasCharging = false;
                ReleaseAttack();
            }
        };

        inputActions.Player.Attack.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction && !wasCharging)
            {
                DoBasicAttack();
            }
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

            if (chargeFillImage != null)
            {
                float percent = Mathf.Clamp01(currentCharge / maxChargeTime);
                chargeFillImage.fillAmount = percent;
            }
        }
    }

    private void StartCharging()
    {
        if (isAttacking) return;

        isCharging = true;
        currentCharge = 0f;

        animator.Play("Attack 2", 0, 0.3f);
        animator.speed = 0f;

        isAttacking = true;
        movementScript.SetIsAttacking(true);

        if (chargeBarInstance == null && chargeBarPrefab != null)
        {
            chargeBarInstance = Instantiate(chargeBarPrefab, transform);
            Transform fillTransform = chargeBarInstance.transform.Find("ChargeBar_Frame/ChargeBar_Background/ChargeBar_Fill");
            if (fillTransform != null)
                chargeFillImage = fillTransform.GetComponent<Image>();
            else
                Debug.LogWarning("No se encontró ChargeFill en el prefab.");

            chargeBarInstance.transform.localPosition = new Vector3(0.5f, 1.5f, 0); // Ajusta si es necesario
        }

        if (chargeBarInstance != null)
        {
            chargeBarInstance.SetActive(true);
            if (chargeFillImage != null)
                chargeFillImage.fillAmount = 0f;
        }

        Debug.Log("Cargando ataque...");
    }

    private void ReleaseAttack()
    {
        isCharging = false;
        animator.speed = 1f;

        float ratio = Mathf.InverseLerp(minChargeTime, maxChargeTime, currentCharge);
        float totalDamage = baseDamage + ratio * maxExtraDamage;

        Debug.Log($"Tiempo de carga: {currentCharge:F2} segundos");
        Debug.Log($"¡Ataque pesado ejecutado! Daño: {totalDamage:F1}");

        currentCharge = 0f;

        if (chargeBarInstance != null)
            chargeBarInstance.SetActive(false);

        Invoke(nameof(EndAttack), 0.8f);
    }

    private void DoBasicAttack()
    {
        if (isAttacking || isCharging) return;

        isAttacking = true;
        movementScript.SetIsAttacking(true);

        animator.SetTrigger("LightAttack");
        Debug.Log("Ataque básico ejecutado");

        Invoke(nameof(EndAttack), 0.6f);
    }

    private void EndAttack()
    {
        isAttacking = false;
        movementScript.SetIsAttacking(false);
    }
}
