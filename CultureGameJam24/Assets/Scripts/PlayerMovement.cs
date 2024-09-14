using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] private Camera cam;
    
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            // Remove any Y component from the forward vector to restrict movement to the XZ plane
            Vector3 flatForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

            // Calculate movement direction based on input
            Vector3 moveDirection = (flatForward * vertical + transform.right * horizontal).normalized;

            // Apply movement
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        
        // Rotation
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * (mouseSensitivity * Time.deltaTime);
        transform.Rotate(transform.up * mouse.x);
        xRotation -= mouse.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
