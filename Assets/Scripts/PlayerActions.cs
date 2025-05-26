using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerActions : MonoBehaviour
{
    public float speed = 2f;           // Movement speed
    public float gravity = -9.81f;      // Gravity value
    public float jumpHeight = 0.3f;       // Jump height
    public float mouseSensitivity = 200f; // Rotation sensitivity for mouse/trackpad

    private CharacterController controller;
    private Vector3 velocity;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Optional: lock cursor for better rotation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Rotation based on Mouse/Trackpad ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX); // Rotate only around Y axis

        // --- Movement ---
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // --- Jump ---
        if (Input.GetKey(KeyCode.Space) && 
            (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
             Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && 
            controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
        }

        // --- Gravity ---
        if (controller.isGrounded && velocity.y < 0 && !isJumping)
        {
            velocity.y = -2f; // Keep grounded
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (velocity.y < 0)
            isJumping = false;
    }
}
