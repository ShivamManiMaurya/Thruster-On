using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustForce, rotateAngle;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void Update()
    {
        
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRocket(rotateAngle);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRocket(-rotateAngle);
        }
    }

    private void RotateRocket(float rotateRocketAngle)
    {
        rb.freezeRotation = true;  // freezing the physics rotation so manual freezing will take over.
        transform.Rotate(Vector3.forward, rotateRocketAngle * Time.deltaTime);
        rb.freezeRotation = false; // Un-freezing the manual rotation so physics system can take over.
    }
}
