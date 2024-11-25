using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;  // Increased for clearer testing
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else{
            audioSource.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating Left"); // Log when A is pressed
            ApplyRotation(rotationThrust); // Rotate positively
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating Right"); // Log when D is pressed
            ApplyRotation(-rotationThrust); // Rotate negatively
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Temporarily disable physics rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); // Adjust this axis if needed
        rb.freezeRotation = false; // Re-enable physics rotation
    }
}