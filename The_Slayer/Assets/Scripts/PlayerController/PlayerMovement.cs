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
    public Camera playerCamera;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    
    [Header("Headbob Parameters")]
    public float walkBobSpeed = 14f;
    public float walkBobAmount = 0.05f;
    public float sprintBobSpeed = 18f;
    public float sprintBobAmount = 0.11f;
    public float crouchBobSpeed = 8f;
    public float crouchBobAmount = 0.025f;
    private float defaultYPos = 0f;
    private float timer;

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
        defaultYPos = playerCamera.transform.localPosition.y;
    }

    void Update()
    {
        // Debugging
        velocity = controller.velocity;
        
        GroundCheck();
        StateHandler();
        ApplyMovement();
        HandleHeadBob();
    }

    public void HandleHeadBob()
    {
        if (!controller.isGrounded) return;

        if (Mathf.Abs(controller.velocity.x) > 0.1f || Mathf.Abs(controller.velocity.x) > 0.1f)
        {
            timer += Time.deltaTime * (state switch
            {
                MovementState.crouching => crouchBobSpeed,
                MovementState.sprinting => sprintBobSpeed,
                _ => walkBobSpeed
            });
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * state switch
                {
                    MovementState.crouching => crouchBobAmount,
                    MovementState.sprinting => sprintBobAmount,
                    _ => walkBobAmount
                },
                playerCamera.transform.localPosition.z);
        }

        if (playerCamera.transform.localPosition.y > defaultYPos + 0.05f)
        {
            float decreaseY = playerCamera.transform.localPosition.y - 0.1f * Time.deltaTime;
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                decreaseY,
                playerCamera.transform.localPosition.z);
        }
        else if (playerCamera.transform.localPosition.y < defaultYPos - 0.05f)
        {
            float increaseY = playerCamera.transform.localPosition.y + 0.1f * Time.deltaTime;
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                increaseY,
                playerCamera.transform.localPosition.z);
        }
    }

    void ApplyMovement()
    {
        if (isGrounded && Input.GetKey(jumpKey))
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }
        else if (!isGrounded)
        {
            gravityVelocity.y += gravity * Time.deltaTime;
        }
        
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * inputZ + transform.right * inputX;
        if (move.magnitude > 1)
        {
            move.Normalize();
        }
        
        controller.Move(move * movementSpeed * Time.deltaTime + gravityVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
