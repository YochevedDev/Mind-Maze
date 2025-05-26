using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script moves the player using arrow keys or WASD.
// Requires a CharacterController to work properly.

[RequireComponent(typeof(CharacterController))]
public class PlayerActions1 : MonoBehaviour
{
    public float speed = 5f;           // Movement speed
    public float gravity = -9.81f;     // Gravity value

    private CharacterController controller;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from keyboard (Arrow keys or WASD)
        float moveX = Input.GetAxis("Horizontal"); // Left/Right or A/D
        float moveZ = Input.GetAxis("Vertical");   // Forward/Back or W/S

        // Convert input into a movement vector based on player's facing direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);

        // Gravity handling
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps player grounded
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
