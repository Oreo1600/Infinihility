using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform cameraRoot;
    [SerializeField] Transform groundCheck;

    [SerializeField] Animator animator;
    int xVelHash;
    int yVelHash;
    float xAnimeVelocity;
    float yAnimeVelocity;
    [SerializeField] float animBlendSpeed = 8.9f;

    [SerializeField] float walkingSpeed;
    [SerializeField] float runningSpeed;
    float moveSpeed;
    Vector2 moveDirection;
    Vector2 mouseDelta;
    bool isGrounded = true;


    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference mouseInput;
    [SerializeField] InputActionReference sprintInput;
    [SerializeField] InputActionReference jumpInput;

    float mouseX, mouseY;
    [SerializeField] float mouseSensitivity = 70f;
    float xRotation = 0;
    Vector3 velocity;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.1f;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float initialCameraFOV;
    [SerializeField] bool isRunning;
    [SerializeField] float airTime;
    bool justLanded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialCameraFOV = playerCamera.GetComponent<Camera>().fieldOfView;

        xVelHash = Animator.StringToHash("X_Velocity");
        yVelHash = Animator.StringToHash("Y_Velocity");
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
            animator.SetTrigger("RunJump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);        }
    }
    void HandleGravity()
    {
        if (!Physics.Raycast(groundCheck.position, Vector3.down, groundDistance,groundMask))
        {
            isGrounded = false;
            airTime += Time.deltaTime;
            justLanded = false;
        }
        else
        {
            isGrounded = true;
            if (!justLanded)
            {
                Landed();
                justLanded = true;
            }
        }
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Falling", !isGrounded); 
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
            if(moveDirection != Vector2.zero)
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, initialCameraFOV + 30f, Time.deltaTime * 5f);
            else
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, initialCameraFOV, Time.deltaTime * 5f);
        }
        else
        {
            moveSpeed = walkingSpeed;

            //if(moveDirection != Vector2.zero)
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, initialCameraFOV, Time.deltaTime * 5f);
        }

        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
        xAnimeVelocity = moveDirection.x * moveSpeed;
        yAnimeVelocity = moveDirection.y * moveSpeed;
        if (xAnimeVelocity > 2f && xAnimeVelocity < 3f) xAnimeVelocity = 3f;
        if (yAnimeVelocity > 2f && yAnimeVelocity < 3f) yAnimeVelocity = 3f;
        if (xAnimeVelocity < -2f && xAnimeVelocity > -3f) xAnimeVelocity = -3f;
        if (yAnimeVelocity < -2f && yAnimeVelocity > -3f) yAnimeVelocity = -3f;
        xAnimeVelocity = Mathf.Lerp(animator.GetFloat(xVelHash), xAnimeVelocity, Time.deltaTime * animBlendSpeed);
        yAnimeVelocity = Mathf.Lerp(animator.GetFloat(yVelHash), yAnimeVelocity, Time.deltaTime * animBlendSpeed);
        animator.SetFloat(xVelHash, xAnimeVelocity);
        animator.SetFloat(yVelHash, yAnimeVelocity);
        if (moveDirection.magnitude > 0.1f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        animator.SetBool("isRunning", isRunning);
    }
    void HandleMouseLook()
    {
        mouseX = mouseDelta.x * Time.deltaTime * mouseSensitivity;
        mouseY = mouseDelta.y * Time.deltaTime * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 65f);
        playerCamera.position = cameraRoot.position;
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Landed()
    {
        animator.SetFloat("airTime", airTime);
        airTime = 0;
        justLanded = true;
    }
}
