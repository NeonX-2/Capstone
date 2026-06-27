using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    // New Input System references
    private Keyboard keyboard;
    private Mouse mouse;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (keyboard == null || mouse == null) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = keyboard.leftShiftKey.isPressed;
        float verticalInput = (keyboard.wKey.isPressed ? 1 : 0) + (keyboard.sKey.isPressed ? -1 : 0);
        float horizontalInput = (keyboard.dKey.isPressed ? 1 : 0) + (keyboard.aKey.isPressed ? -1 : 0);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * verticalInput : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * horizontalInput : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (keyboard.spaceKey.isPressed && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (keyboard.rKey.isPressed && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
