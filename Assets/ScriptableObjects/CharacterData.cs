using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Character Data", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Vitals")]
    public float maxHealth = 100f;

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 50f;
    public float deceleration = 40f;

    [Header("Jump")]
    public float jumpForce = 16f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.6f;
}
