using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3;
    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;
    CharacterController controller;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 5f;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }

    void GetDirectionAndMove()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;
        
        hzInput = (keyboard.dKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed ? 1 : 0);
        vInput = (keyboard.wKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed ? 1 : 0);

        dir = transform.forward * vInput + transform.right * hzInput;
        
        // Jump
        if (keyboard.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            velocity.y = jumpForce;
        }
    }
    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
            return false;
    }
    void Gravity()
    {
        if (IsGrounded() && velocity.y < 0) velocity.y = 0;
        else velocity.y += gravity * Time.deltaTime;
        
        // Combine horizontal movement and vertical velocity
        Vector3 movement = (dir * moveSpeed) + new Vector3(0, velocity.y, 0);
        controller.Move(movement * Time.deltaTime);
    }
}

