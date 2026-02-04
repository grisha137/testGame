using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 5f;

    private HealthSystem healthSystem;
    private Rigidbody2D body;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        body = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamage(float amount, Vector2 sourcePosition)
    {
        if (healthSystem == null)
        {
            return;
        }

        healthSystem.TakeDamage(amount);

        if (body != null)
        {
            Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;
            body.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
