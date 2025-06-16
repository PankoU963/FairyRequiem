using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboSystem : MonoBehaviour
{
    public float comboResetTime = 1.0f; // Tiempo máximo entre golpes para continuar el combo
    public float inputBufferTime = 0.1f; // Tiempo para considerar un input como parte del combo

    [SerializeField] private int comboStep = 0;
    [SerializeField] private float lastAttackTime = 0f;
    [SerializeField] private bool isAttacking = false;

    private bool pendingAttack = false;
    private float attackBufferTimer = 0f;

    //public int comboCount;
    //public float timerCombo;
    public bool inputBuffered;

    public Animator animator;
    //public bool isAttack;

    //[SerializeField] private float comboResetTime = 1f;

    public AnimatorStateInfo stateInfo;

    void Update()
    {

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetBool("Attack", isAttacking);

        // Si se detecta la tecla X, empieza el buffer
        if (Input.GetKeyDown(KeyCode.X))
        {
            pendingAttack = true;
            attackBufferTimer = inputBufferTime;
        }

        // Procesar el ataque si hay uno pendiente
        if (pendingAttack)
        {
            attackBufferTimer -= Time.deltaTime;

            if (attackBufferTimer <= 0f)
            {
                pendingAttack = false;

                if (Input.GetKey(KeyCode.Space))
                {
                    ExecuteSpecialAttack();
                }
                else
                {
                    if(stateInfo.IsName("Movement") && comboStep == 0) ExecuteComboStep();
                    if(stateInfo.IsName("Attack 1") && comboStep == 1) ExecuteComboStep();
                    if(stateInfo.IsName("Attack 2") && comboStep == 2) ExecuteComboStep();
                    if(stateInfo.IsName("Attack 3") && comboStep == 3) ExecuteComboStep();
                }
            }
        }

        if (!stateInfo.IsName("Movement"))
        {
            comboResetTime = stateInfo.length + 0.5f;
        }

        // Reset de combo por inactividad
        if (isAttacking && Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
            isAttacking = false;
            Debug.Log("Combo reiniciado por inactividad");
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
        //if(stateInfo.IsName("Movement"))
        //{
        //    ExecuteCombo(comboStep);
        //}
        //if (!stateInfo.IsName("Movement") && stateInfo.normalizedTime >= 0.8f && !inputBuffered)
        //{
        //    ExecuteCombo(comboStep);
        //}
    }


    void ExecuteCombo(int step)
    {
        lastAttackTime = Time.time;
        isAttacking = true;



        switch (step)
        {
            case 0:
                Debug.Log("Ataque 1: Golpe rápido");
                comboStep = 1;
                animator.SetInteger("ComboStep", comboStep);
                break;
            case 1:
                Debug.Log("Ataque 1: Golpe rápido");
                animator.SetInteger("ComboStep", comboStep);
                break;
            case 2:
                Debug.Log("Ataque 2: Golpe cruzado");
                animator.SetInteger("ComboStep", comboStep);
                break;
            case 3:
                Debug.Log("Ataque 3: Ataque fuerte");
                animator.SetInteger("ComboStep", comboStep);
                break;
            default:
                Debug.Log("Error en combo");
                break;
        }
    }

    void ExecuteSpecialAttack()
    {
        Debug.Log("Ataque especial: Golpe ascendente");
        isAttacking = false; // No afecta el combo
        comboStep = 0;       // También reinicia el combo si era parte de uno
    }

    //public void Attack()
    //{
    //    if (!stateInfo.IsName("Movement"))
    //    {
    //        comboResetTime = stateInfo.length;
    //    }

    //    if (!isAttack && Mouse.current.leftButton.wasPressedThisFrame)
    //    {
    //        comboCount = 1;
    //        animator.SetInteger("ComboStep", comboCount);
    //        isAttack = true;
    //        timerCombo = 0f;


    //    }

    //    // Si estamos en ataque, cuenta tiempo
    //    if (isAttack)
    //    {
    //        timerCombo += Time.deltaTime;

    //        // Si presionan el botón durante la animación y estamos en ventana válida
    //        if (Mouse.current.leftButton.wasPressedThisFrame && stateInfo.normalizedTime >= 0.6f && stateInfo.normalizedTime < 0.9f)
    //        {
    //            inputBuffered = true;
    //        }

    //        // Revisar por combo continuación
    //        if (stateInfo.IsName("Attack 1") && stateInfo.normalizedTime >= 0.9f && comboCount == 1 && inputBuffered)
    //        {

    //            comboCount = 2;
    //            animator.SetInteger("ComboStep", comboCount);
    //            inputBuffered = false;
    //            timerCombo = 0f;

    //        }
    //        else if (stateInfo.IsName("Attack 2") && stateInfo.normalizedTime >= 0.9f && comboCount == 2 && inputBuffered)
    //        {
    //            comboCount = 3;
    //            animator.SetInteger("ComboStep", comboCount);
    //            inputBuffered = false;
    //            timerCombo = 0f;


    //        }


    //        // Si termina el último ataque o se pasa el tiempo sin combo, reset
    //        if (stateInfo.IsName("Attack 3") && stateInfo.normalizedTime >= 0.9f || timerCombo >= comboResetTime)
    //        {
    //            comboCount = 0;
    //            isAttack = false;
    //            inputBuffered = false;
    //            animator.SetInteger("ComboStep", 0);
    //        }
    //    }
    //}
}