using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform groundCheck;

    [SerializeField] float walkingSpeed;
    [SerializeField] float runningSpeed;
    float moveSpeed;
    Vector2 moveDirection;
    Vector2 mouseDelta;
    [SerializeField] bool isGrounded = true;

    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference mouseInput;
    [SerializeField] InputActionReference sprintInput;
    [SerializeField] InputActionReference jumpInput;

    float mouseX, mouseY;
    [SerializeField] float mouseSensitivity = 70f;
    float xRotation = 0;
    [SerializeField] Vector3 velocity;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.1f;
    [SerializeField] float jumpHeight = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveInput.action.ReadValue<Vector2>();
        mouseDelta = mouseInput.action.ReadValue<Vector2>();

        HandleGravity();
        HandleMovement();
        HandleMouseLook();
        HandleJump();
    }

    void HandleJump()
    {
        if (jumpInput.action.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
    void HandleGravity()
    {
        if (!Physics.Raycast(groundCheck.position, Vector3.down, groundDistance,groundMask))
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        if (!isGrounded)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -0.2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    void HandleMovement()
    {
        if (sprintInput.action.IsPressed())
        {
            moveSpeed = runningSpeed;
        }
        else
        {
            moveSpeed = walkingSpeed;
        }

        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
    void HandleMouseLook()
    {
        mouseX = mouseDelta.x * Time.deltaTime * mouseSensitivity;
        mouseY = mouseDelta.y * Time.deltaTime * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
