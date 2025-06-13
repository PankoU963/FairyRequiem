using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public PlayerInput playerInput;
    public CharacterController characterController;

    private Vector3 playerVelocity;
    private Vector3 moveDirection;

    public float moveSpeedX = 5f;
    public float moveSpeedY = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float rotationSpeed = 10f;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction dashAction;

    private Vector2 movementInput;

    // Dash variables
    [SerializeField] private float dashSpeedMultiplier = 2f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 0.8f;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        dashAction = playerInput.actions["Dash"];

        moveAction.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => movementInput = Vector2.zero;
        dashAction.performed += ctx => TryDash();
    }

    void Update()
    {
        HandleDashTimers();
        Move();
    }

    private void HandleDashTimers()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }
        }
        else if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void TryDash()
    {
        if (!isDashing && dashCooldownTimer <= 0 && characterController.isGrounded)
        {
            isDashing = true;
            dashTimer = dashDuration;

            // Dash en la dirección actual de movimiento o la última dirección
            Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);
            if (moveDir == Vector3.zero)
                moveDir = transform.forward; // dash hacia adelante si no hay input

            dashDirection = moveDir.normalized;
        }
    }

    private void Move()
    {
        Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);

        if (!isDashing)
        {
            playerVelocity.x = moveDir.x * moveSpeedX;
            playerVelocity.z = moveDir.z * moveSpeedY;
        }
        else
        {
            playerVelocity.x = dashDirection.x * dashSpeedMultiplier * moveSpeedX;
            playerVelocity.z = dashDirection.z * dashSpeedMultiplier * moveSpeedY;
        }

        if (moveDir.magnitude > 0 && !isDashing)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (characterController.isGrounded)
        {
            playerVelocity.y = -2f;
            if (jumpAction.triggered && !isDashing)
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
