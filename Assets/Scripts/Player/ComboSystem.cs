using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private float comboResetTime = 1.0f;
    [SerializeField] private float inputBufferTime = 0.1f;
    [SerializeField] private int comboStep = 0;
    [SerializeField] private float lastAttackTime = 0f;
    [SerializeField] private bool isAttacking = false;
    private bool pendingAttack = false;
    private float attackBufferTimer = 0f;
    private  Animator animator;
    private AnimatorStateInfo stateInfo;

    private Movement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Ability"))
        {
            playerMovement.isAttack = isAttacking;
        }
        animator.SetBool("Attack", isAttacking);

        if (Input.GetKeyDown(KeyCode.X))
        {
            pendingAttack = true;
            attackBufferTimer = inputBufferTime;
        }

        if (pendingAttack)
        {
            attackBufferTimer -= Time.deltaTime;

            if (attackBufferTimer <= 0f)
            {
                pendingAttack = false;

                if (Input.GetKey(KeyCode.Space))
                {
                    ExecuteJumpAttack();
                }
                else
                {
                    if ((stateInfo.IsName("Movement") || stateInfo.IsName("Idle")) && comboStep == 0) ExecuteComboStep();
                    else if (stateInfo.IsName("Attack 1") && comboStep == 1) ExecuteComboStep();
                    else if (stateInfo.IsName("Attack 2") && comboStep == 2) ExecuteComboStep();
                    else if (stateInfo.IsName("Attack 3") && comboStep == 3) ExecuteComboStep();
                    else lastAttackTime = Time.time;
                }
            }
        }

        if (!stateInfo.IsName("Movement"))
        {
            comboResetTime = stateInfo.length + 0.5f;
        }

        if (isAttacking && Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
            isAttacking = false;
        }
        if (stateInfo.IsName("Attack 3") && stateInfo.normalizedTime >= 0.95f && comboStep == 3)
        {
            comboStep = 0;
            isAttacking = false;
        }
    }

    void ExecuteComboStep()
    {
        if (!isAttacking)
        {
            comboStep = 1;
        }
        else if (Time.time - lastAttackTime <= comboResetTime)
        {
            comboStep++;
            if (comboStep > 3)
                comboStep = 0;
        }
        else
        {
            comboStep = 1;
        }
        ExecuteCombo(comboStep);
    }

    void ExecuteCombo(int step)
    {
        lastAttackTime = Time.time;
        isAttacking = true;

        switch (step)
        {
            case 0:
                //Debug.Log("Ataque 1: Golpe rápido");
                comboStep = 1;
                animator.SetInteger("ComboStep", comboStep);
                break;
            case 1:
                //Debug.Log("Ataque 1: Golpe rápido");
                animator.SetInteger("ComboStep", comboStep);
                break;
            case 2:
                //Debug.Log("Ataque 2: Golpe cruzado");
                animator.SetInteger("ComboStep", comboStep);
                break;
            case 3:
                //Debug.Log("Ataque 3: Ataque fuerte");
                animator.SetInteger("ComboStep", comboStep);
                break;
            default:
                //Debug.Log("Error en combo");
                break;
        }
    }

    void ExecuteJumpAttack()
    {
        isAttacking = false; // No afecta el combo
        comboStep = 0;       // También reinicia el combo si era parte de uno
    }
}