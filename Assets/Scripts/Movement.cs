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
    }

    private void Move()
    {
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