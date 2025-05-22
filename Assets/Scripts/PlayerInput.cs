using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    // Player movement speed
    [SerializeField] 
    private float moveSpeed = 5f;

    // Mouse look sensitivity
    [SerializeField] 
    private float lookSpeed = 2f;

    // Height of the jump
    [SerializeField] 
    private float jumpHeight = 2f;

    // Reference to the player’s camera
    [SerializeField] 
    private Transform playerCamera;

    // Reference to CharacterController component
    private CharacterController controller;

    // Player's velocity (used for gravity & jumping)
    private Vector3 velocity;

    // Is the player currently grounded?
    private bool isGrounded;

    // Vertical rotation value for camera (pitch)
    private float xRotation = 0f;                         

    private void Awake()
    {
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the player is grounded (standing on something)
        isGrounded = controller.isGrounded;

        // Reset downward velocity when grounded to avoid accumulating gravity force
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;  // Small negative to keep grounded reliably

        // Get horizontal and vertical input (WASD/Arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to player's current rotation
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the player controller based on input, speed, and frame time
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Handle jump input; only allow jump if grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);

        // Apply gravity to vertical velocity each frame
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Move the player vertically based on gravity and jump velocity
        controller.Move(velocity * Time.deltaTime);

        // Get mouse input for looking around (mouse movement)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the player horizontally (yaw) based on mouse X movement
        transform.Rotate(Vector3.up * mouseX);

        // Adjust vertical rotation (pitch) for the camera and clamp to avoid flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera (only pitch)
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}