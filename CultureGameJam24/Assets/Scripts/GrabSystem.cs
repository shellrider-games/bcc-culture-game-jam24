using System;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    [SerializeField] private string grabTag;
    [SerializeField] private float grabRange;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] float maxSpeed = 10f;
    
    public float springConstant = 50f;   // How stiff the "spring" is (higher value = faster movement)
    public float dampingFactor = 10f;    // Damping factor to prevent oscillation/overshoot
    public float stopDistance = 0.01f;   // Distance at which to stop moving
    
    private bool grabbed = false;
    private GameObject grabbedObject;
    private Rigidbody grabbedRb;
    private Camera cam;
    
    public event Action<GameObject> OnRelease;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (grabbed) MoveGrabbedObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ReleaseGrab();
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out var hit, grabRange))
        {
            if (hit.collider.CompareTag(grabTag))
            {
                grabbed = true;
                grabbedObject = hit.collider.gameObject;
                grabbedRb = grabbedObject.GetComponent<Rigidbody>();
                grabbedRb.useGravity = false;
            }
        }        
    }

    void ReleaseGrab()
    {
        grabbed = false;
        OnRelease?.Invoke(grabbedObject);
        grabbedRb.useGravity = true;
        grabbedRb = null;
        grabbedObject = null;
    }

    void MoveGrabbedObject()
    {
        // Calculate position difference (from object to target)
        Vector3 displacement = grabPoint.position - grabbedObject.transform.position;

        // Calculate velocity difference (damping term)
        Vector3 velocity = grabbedRb.velocity;

        // If the object is within the stop distance, stop moving
        if (displacement.magnitude > stopDistance)
        {
            // Spring force based on displacement (Hooke's Law: F = -kx)
            Vector3 springForce = springConstant * displacement;

            // Damping force based on velocity (F = -cv)
            Vector3 dampingForce = -dampingFactor * velocity;

            // Apply the total force to the Rigidbody
            grabbedRb.AddForce(springForce + dampingForce, ForceMode.Force);
        }
        else
        {
            // When near the target, we reduce the velocity to zero to stop
            grabbedRb.velocity = Vector3.zero;
        }
    }
}
