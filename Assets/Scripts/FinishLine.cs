using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f; // Delay before reloading the scene in seconds
    [SerializeField] private ParticleSystem finishEffect; // Particle system for the finish line effect
   void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
           Debug.Log("Player has crossed the finish line!");
           finishEffect.Play(); // Play the finish line particle effect
           Invoke(nameof(ReloadScene), reloadDelay); // Reload the scene after the specified delay
       }
   }

   void ReloadScene()
   {
        // Reload the current scene
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
