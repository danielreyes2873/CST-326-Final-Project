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

    [Header("Properties")]
    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;

    [Header("Private")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 gravityVelocity;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // Debugging
        velocity = controller.velocity;
        
        HandleGravity();
        GroundCheck();
        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (isGrounded && gravityVelocity.y < 0 && Input.GetButton("Jump"))
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

        controller.Move(move * speed * Time.deltaTime + gravityVelocity * Time.deltaTime);
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

    // Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
