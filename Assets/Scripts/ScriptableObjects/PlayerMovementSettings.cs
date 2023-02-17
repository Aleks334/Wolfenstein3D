using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player_Movement_Settings")]
public class PlayerMovementSettings : ScriptableObject
{
    [Header("Default player movement speed")]
    public float mvmtSpeed = 12f;

    [Header("Rates for player movement types")]
    public float runningRate = 1.8f;
    public float strafingRate = 1.4f;

    [Header("Sensitivity for rotation")]
    public float sensitivity = 150f;
}
