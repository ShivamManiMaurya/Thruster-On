using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustForce, rotateAngle;
    [SerializeField] private AudioClip thrustSound;
    [SerializeField] private ParticleSystem MainThruster, leftThruster, rightThruster;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MainThrustOn();
        }
        else
        {
            audioSource.Stop();
            MainThruster.Stop();
        }
    }

    private void MainThrustOn()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }
        if (!MainThruster.isPlaying)
        {
            MainThruster.Play();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftThrustOn();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightThrustOn();
        }
        else
        {
            leftThruster.Stop();
            rightThruster.Stop();
        }
    }

    private void RightThrustOn()
    {
        RotateRocket(-rotateAngle);
        if (!rightThruster.isPlaying)
        {
            rightThruster.Play();
        }
    }

    private void LeftThrustOn()
    {
        RotateRocket(rotateAngle);
        if (!leftThruster.isPlaying)
        {
            leftThruster.Play();
        }
    }

    private void RotateRocket(float rotateRocketAngle)
    {
        rb.freezeRotation = true;  // freezing the physics rotation so manual freezing will take over.
        transform.Rotate(Vector3.forward, rotateRocketAngle * Time.deltaTime);
        rb.freezeRotation = false; // Un-freezing the manual rotation so physics system can take over.
    }
}
