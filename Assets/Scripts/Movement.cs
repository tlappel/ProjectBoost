using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /*
        PARAMETERS - tuning, typically set in the editor
        CACHE      - reference for readability or speed
        STATE      - Private instance (member) variables
    */
    Rigidbody rb; //cache reference to rigidbody
    
    [SerializeField] AudioClip thrustFX;
    [SerializeField] ParticleSystem mainEngineParts;
    [SerializeField] ParticleSystem leftThrustParts;
    [SerializeField] ParticleSystem rightThrustParts;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    // Start is called before the first frame update

    AudioSource audioSource;
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrust();
        }

    }

    private void StopThrust()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        mainEngineParts.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(thrustFX);
        if (!mainEngineParts.isPlaying)
            mainEngineParts.Play();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
            ThrustLeft();
        }
        else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            ThrustRight();
        }
    }

    private void ThrustRight()
    {
        //spin to the right
        //Debug.Log("Rotate Right");
        ApplyRotation(-rotationThrust);
        leftThrustParts.Play();
    }

    private void ThrustLeft()
    {
        //Spin to the left
        //Debug.Log("Rotate Left");
        ApplyRotation(rotationThrust);
        rightThrustParts.Play();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
