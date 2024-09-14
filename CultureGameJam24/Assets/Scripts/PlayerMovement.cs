using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] private Camera cam;
    
    private Vector2 movement;
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Remove any Y component from the forward vector to restrict movement to the XZ plane
        Vector3 flatForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

        // Calculate movement direction based on input
        Vector3 moveDirection = (flatForward * movement.y + transform.right * movement.x).normalized;

        // Apply movement
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        var mouse = context.ReadValue<Vector2>() * mouseSensitivity;
        
        transform.Rotate(transform.up * mouse.x);
        xRotation -= mouse.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
