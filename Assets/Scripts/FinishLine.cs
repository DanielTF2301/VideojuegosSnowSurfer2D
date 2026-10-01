using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
           Debug.Log("Player has crossed the finish line!");
           //TODO: You can add additional logic here, such as triggering a win condition or loading a new scene.
           Invoke(nameof(ReloadScene), 1f); // Reload the scene after 1 seconds
       }
   }

   void ReloadScene()
   {
        // Reload the current scene
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
