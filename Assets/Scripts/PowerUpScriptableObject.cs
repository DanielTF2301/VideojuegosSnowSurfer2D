using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUps/PowerUps Data")]
public class PowerUpScriptableObject : ScriptableObject
{
    [SerializeField] private string powerUpType; // Type of the power-up (e.g., "Boost", "Shield", etc.)
    [SerializeField] private float powerUpValue; // Value associated with the power-up (e.g., speed boost amount, shield strength, etc.)
    [SerializeField] private float timeLimit; // Duration of the power-up effect in seconds

    public string PowerUpType { get => powerUpType; set => powerUpType = value; }
    public float PowerUpValue { get => powerUpValue; set => powerUpValue = value; }
    public float TimeLimit { get => timeLimit; set => timeLimit = value; }
}
