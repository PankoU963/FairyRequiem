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
    private bool isAttacking = false;

    private InputSystem_Actions inputActions;

    [SerializeField] private Animator animator;

    private Movement movementScript;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        movementScript = GetComponent<Movement>();

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
        if (isAttacking || isCharging) return; //Avoid movement if is Attacking

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
        animator.SetTrigger("HeavyAttack");
        animator.speed = 0f;
        Debug.Log("Cargando ataque...");

    }

    private void ReleaseAttack()
    {
        isCharging = false;
        animator.speed = 1f;

        isAttacking = true;
        movementScript.SetIsAttacking(true);

        Invoke(nameof(EndAttack), 1f); 
        
        float ratio = Mathf.InverseLerp(minChargeTime, maxChargeTime, currentCharge);
        float totalDamage = baseDamage + ratio * maxExtraDamage;
        Debug.Log($"¡Ataque pesado ejecutado! Daño: {totalDamage:F1}");
        currentCharge = 0f;
    }

    private void DoBasicAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        animator.SetTrigger("LightAttack");
        Debug.Log("Ataque básico ejecutado");

        Invoke(nameof(EndAttack), 0.6f); // ajusta según duración
    }

    private void EndAttack()
    {
        isAttacking = false;
        movementScript.SetIsAttacking(false);
    }
}
