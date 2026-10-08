using System;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpScriptableObject powerUpData; // Reference to the PowerUpScriptableObject
    PlayerController playerController; // Reference to the PlayerController script
    SpriteRenderer powerUpSpriteRenderer; // Reference to the player's SpriteRenderer
    float timeLeft;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        powerUpSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        timeLeft = powerUpData.TimeLimit; // Initialize the time left for the power-up effect
    }

    private void Update()
    {
        CountDownPowerUpTime();
    }

    private void CountDownPowerUpTime()
    {
        if (powerUpSpriteRenderer.enabled == false)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime; // Decrease the time left for the power-up effect
                if (timeLeft <= 0)
                {
                    playerController.DeactivatePowerUp(powerUpData); // Deactivate the power-up effect on the player
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && powerUpSpriteRenderer.enabled)
        {
            powerUpSpriteRenderer.enabled = false; // Disable the power-up sprite renderer to hide the power-up
            playerController.ApplyPowerUp(powerUpData); // Apply the power-up effect to the player
        }
        
    }
}
