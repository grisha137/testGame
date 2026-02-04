using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 5f;

    private HealthSystem healthSystem;
    private Rigidbody2D body;
    private PlayerController playerController;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        body = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    public void ApplyDamage(float amount, Vector2 sourcePosition)
    {
        if (playerController != null)
        {
            if (playerController.IsInvulnerable)
            {
                return;
            }

            playerController.ApplyDamage(amount);
        }
        else
        {
            if (healthSystem == null || healthSystem.IsDead)
            {
                return;
            }

            healthSystem.TakeDamage(amount);
        }

        if (body != null)
        {
            Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;
            body.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
