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
            //Debug.Log("Pressed space, thrusting1");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            audioSource = GetComponent<AudioSource>();
            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(thrustFX);
            
        }
        else
        {
            if(audioSource.isPlaying)
                audioSource.Stop();
        }
        
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
            //Spin to the left
            //Debug.Log("Rotate Left");
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            //spin to the right
            //Debug.Log("Rotate Right");
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
