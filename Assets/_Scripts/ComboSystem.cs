using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public float comboResetTime = 1.0f; // Tiempo máximo entre golpes para continuar el combo
    public float inputBufferTime = 0.1f; // Tiempo para considerar un input como parte del combo

    private int comboStep = 0;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;

    private bool pendingAttack = false;
    private float attackBufferTimer = 0f;


    void Update()
    {
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
                    ExecuteComboStep();
                }
            }
        }

        // Reset de combo por inactividad
        if (isAttacking && Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
            isAttacking = false;
            Debug.Log("Combo reiniciado por inactividad");
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
                comboStep = 1;
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
            case 1:
                Debug.Log("Ataque 1: Golpe rápido");
                break;
            case 2:
                Debug.Log("Ataque 2: Golpe cruzado");
                break;
            case 3:
                Debug.Log("Ataque 3: Ataque fuerte");
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
}