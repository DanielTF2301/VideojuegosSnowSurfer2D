using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 35f;
    [SerializeField] private ParticleSystem snowEffect; // Particle system for the snow effect
    [SerializeField] private ParticleSystem boostEffect; // Particle system for the boost effect
    [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager script

    private bool canControlPlayer = true; // Flag to control player input
    SurfaceEffector2D surfaceEffector2D;
    float baseSpeed;
    InputAction moveAction;
    Vector2 moveInput;
    float previousRotation; // Variable to store the previous rotation of the player
    float totalRotation; // Variable to store the total rotation of the player
    int flipCount; // Variable to store the number of flips performed by the player
    Rigidbody2D rb;

    public bool CanControlPlayer { get => canControlPlayer; set => canControlPlayer = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector2D.speed; // Store the base speed of the SurfaceEffector2D
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControlPlayer) return; // If player control is disabled, exit the Update method
        PlayerTorque();
        BoostPlayer();
        CalculateFlips();
    }

    /// <summary>
    /// Calculates the number of flips the player has performed based on their rotation.
    /// </summary>
    private void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z; // Get the current rotation of the player in degrees
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation); // Calculate the change in rotation since the last frame
        if (Mathf.Abs(totalRotation) >= 340) // Check if the total rotation exceeds 360 degrees
        {
            flipCount++; // Increment the flip count
            scoreManager.AddScore(flipCount*100); // Update the score based on the number of flips
            totalRotation = 0; // Reset the total rotation
        }
        previousRotation = currentRotation; // Update the previous rotation for the next frame
    }

    /// <summary>
    /// Applies torque to the player based on input from the Move action.
    /// </summary>
    void PlayerTorque()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x < 0)
        {
            rb.AddTorque(torqueAmount);
        }
        else if (moveInput.x > 0)
        {
            rb.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        // Increase the player's velocity when the "Boost" action is performed
        if (moveInput.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layerIndex)
        {
            if (moveInput.y > 0)
            {
                boostEffect.Play(); // Play the boost particle effect when boosting on the floor
            }
            else
            {
                snowEffect.Play(); // Play the snow particle effect
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layerIndex)
        {
            Debug.Log("Player has left the floor");
            snowEffect.Stop(); // Stop the snow particle effect
            boostEffect.Stop(); // Stop the boost particle effect
        }
    }
}
