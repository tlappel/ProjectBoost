using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashFX;
    [SerializeField] AudioClip successFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    bool isTransitioning = false;
    bool isCollisionDisabled = false;
    AudioSource audioSource;
   void Start()
    {
       audioSource = GetComponent<AudioSource>();
       isTransitioning = false;
    } 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            //Level Up!
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            //Disable Collisions!
            isCollisionDisabled = !isCollisionDisabled;
        }
    }
   void OnTriggerEnter(Collider other) 
   {
    if(other.gameObject.tag == "Fuel")
    {
        ClearFuel(other);
    }
   }
   
   private void OnCollisionEnter(Collision other) 
   {
    if(isTransitioning||isCollisionDisabled) return;
     switch (other.gameObject.tag)
     {
        case "Friendly": 
            Debug.Log("oops, bumped into a friendly");
            break;
        case "Finish":
            Debug.Log("Yay, finish!");
            StartSuccessSequence();
            break;
        default:
            Debug.Log("Sorry, you blew up");
            
            StartCrashSequence();
            break;
     }

   }

    private void StartSuccessSequence()
    {
        //LoadNextLevel();
        audioSource.Stop();
        audioSource.PlayOneShot(successFX);
        successParticles.Play();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        
        audioSource.Stop();
        audioSource.PlayOneShot(crashFX);
        crashParticles.Play();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }

    void LoadNextLevel()
   {
        isCollisionDisabled = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Current Scene(first part of method): "+currentSceneIndex);
        int cnt = SceneManager.sceneCount;
        //Debug.Log("Scene count: "+ cnt);
        if(currentSceneIndex < cnt)
        {
            currentSceneIndex++;
            //Debug.Log("Current Scene(after add): "+currentSceneIndex);
        }
        else
        {
            currentSceneIndex = 0;
             //Debug.Log("Current Scene(reset): "+currentSceneIndex);
        }
        isTransitioning = false;
        SceneManager.LoadScene(currentSceneIndex);
   }
   void ReloadLevel()
   {
        isCollisionDisabled = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        isTransitioning = false;
        SceneManager.LoadScene(currentSceneIndex);
   }
   void ClearFuel(Collider other)
   {
        Destroy(other.gameObject);   
        Debug.Log("More Fuel Added!"); 
   }
}
