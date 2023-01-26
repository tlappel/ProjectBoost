using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
   void OnTriggerEnter(Collider other) 
   {
    if(other.gameObject.tag == "Fuel")
    {
        ClearFuel(other);
    }
   }
   
   private void OnCollisionEnter(Collision other) 
   {
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
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        //TODO add SFX upon crash
        //TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }

    void LoadNextLevel()
   {
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
        SceneManager.LoadScene(currentSceneIndex);
   }
   void ReloadLevel()
   {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
   }
   void ClearFuel(Collider other)
   {
        Destroy(other.gameObject);   
        Debug.Log("More Fuel Added!"); 
   }
}
