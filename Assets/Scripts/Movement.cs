using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb; //cache reference to rigidbody
    AudioSource rockSound;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       rockSound = GetComponent<AudioSource>();
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
            if(!rockSound.isPlaying)
                rockSound.Play();
            
        }
        else
        {
            if(rockSound.isPlaying)
                rockSound.Stop();
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
