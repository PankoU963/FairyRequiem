using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public PlayerInput playerInput;
    public CharacterController characterController;

    public Animator animator;
    public bool isMoving;
    public bool isAttack;

    public int comboCount;
    public float timerCombo;
    public bool inputBuffered;

    [SerializeField] private float comboResetTime = 1f;

    private Vector3 playerVelocity;
    private Vector3 moveDirection;

    public float moveSpeedX = 5f;
    public float moveSpeedY = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float rotationSpeed = 10f;

    public InputAction moveAction;
    public InputAction jumpAction;

    private Vector2 movementInput;

    public AnimatorStateInfo stateInfo;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
       
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        moveAction.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movementInput = Vector2.zero;

    }

    void Update()
    {
        Move();
        Animations();
        Attack();
    }

    private void Move()
    {
        if (!isAttack) 
        {
            isMoving = movementInput != Vector2.zero ? true : false;

            Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);

            playerVelocity.x = moveDir.x * moveSpeedX;
            playerVelocity.z = moveDir.z * moveSpeedY;

            if (moveDir.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (characterController.isGrounded)
            {
                playerVelocity.y = -2f;

                if (jumpAction.triggered)
                {
                    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            else
            {
                playerVelocity.y += gravity * Time.deltaTime;
            }
            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void Animations()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isMoving)
        {
            animator.SetFloat("Move", 1f);
        }
        else
        {
            animator.SetFloat("Move", 0f);
        }

        if (isAttack && comboCount == 1)
        {
            animator.SetBool("Attack", true);
        }

    }

    public void Attack()
    {
        if (!stateInfo.IsName("Movement"))
        {
            comboResetTime = stateInfo.length;
        }

        if (!isAttack && Mouse.current.leftButton.wasPressedThisFrame)
        {
            comboCount = 1;
            animator.SetInteger("ComboStep", comboCount);
            isAttack = true;
            timerCombo = 0f;


        }

        // Si estamos en ataque, cuenta tiempo
        if (isAttack)
        {
            timerCombo += Time.deltaTime;

            // Si presionan el botón durante la animación y estamos en ventana válida
            if (Mouse.current.leftButton.wasPressedThisFrame && stateInfo.normalizedTime >= 0.6f && stateInfo.normalizedTime < 0.9f)
            {
                inputBuffered = true;
            }

            // Revisar por combo continuación
            if (stateInfo.IsName("Attack 1") && stateInfo.normalizedTime >= 0.9f && comboCount == 1 && inputBuffered)
            {

                comboCount = 2;
                animator.SetInteger("ComboStep", comboCount);
                inputBuffered = false;
                timerCombo = 0f;

            }
            else if (stateInfo.IsName("Attack 2") &&  stateInfo.normalizedTime >= 0.9f && comboCount == 2 && inputBuffered)
            {
                comboCount = 3;
                animator.SetInteger("ComboStep", comboCount);
                inputBuffered = false;
                timerCombo = 0f;


            }
            

            // Si termina el último ataque o se pasa el tiempo sin combo, reset
            if (stateInfo.IsName("Attack 3") && stateInfo.normalizedTime >= 0.9f || timerCombo >= comboResetTime)
            {
                comboCount = 0;
                isAttack = false;
                inputBuffered = false;
                animator.SetInteger("ComboStep", 0);
            }
        }
    }
}