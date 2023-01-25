using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
            break;
        default:
            Debug.Log("Sorry, you blew up");
            ReloadLevel();
            break;
     }

   }
   void ReloadLevel()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   void ClearFuel(Collider other)
   {
        Destroy(other.gameObject);   
        Debug.Log("More Fuel Added!"); 
   }
}
