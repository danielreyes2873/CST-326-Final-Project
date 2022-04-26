using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Set-up")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Properties")]
    public float walkSpd = 10f;
    public float sprintSpd = 15f;
    public float crouchSpd = 5f;
    public float jumpHeight = 5f;
    public float gravity = -9.81f;

    [Header("Movement State")]
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    [Header("Private/Debugging")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 gravityVelocity;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float characterHeight;
    [SerializeField] private float crouchHeight = 1f;

    void Awake()
    {
        characterHeight = controller.height;
    }

    void Update()
    {
        // Debugging
        velocity = controller.velocity;
        
        StateHandler();
        HandleGravity();
        GroundCheck();
        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (isGrounded && gravityVelocity.y < 0 && Input.GetKey(jumpKey))
        {
            ApplyJump();
        }
        else if (isGrounded && gravityVelocity.y < 0)
        {
            // Reset gravity velocity
            gravityVelocity.y = -2f;
        }
        
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * inputZ + transform.right * inputX;

        controller.Move(move * movementSpeed * Time.deltaTime + gravityVelocity * Time.deltaTime);
    }

    void HandleGravity()
    {
        gravityVelocity.y += gravity * Time.deltaTime;
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void ApplyJump()
    {
        gravityVelocity.y += jumpHeight;
    }

    void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            movementSpeed = crouchSpd;
            controller.height = crouchHeight;
        }
        // Mode - Sprinting
        else if(isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            movementSpeed = sprintSpd;
            controller.height = characterHeight;
        }
        // Mode - Walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            movementSpeed = walkSpd;
            controller.height = characterHeight;
        }
        // Mode - Air
        else
        {
            state = MovementState.air;
            movementSpeed = walkSpd;
            controller.height = characterHeight;
        }
    }

    // Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
