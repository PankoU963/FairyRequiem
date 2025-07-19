using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isMoving;
    [SerializeField] public bool isAttack;

    private Vector3 playerVelocity;
    private Vector3 moveDirection;

    [SerializeField] private float moveSpeedX = 5f;
    [SerializeField] private float moveSpeedY = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction jumpAction;
    private Vector2 movementInput;

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
    }

    private void Move()
    {
        if (!isAttack)
        {
            isMoving = movementInput != Vector2.zero;

            
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
        if (isMoving)
        {
            animator.SetFloat("Move", 1f);
        }
        else
        {
            animator.SetFloat("Move", 0f);
        }
    }

    public void SetIsAttacking(bool value)
    {
        isAttack = value;
    }
}