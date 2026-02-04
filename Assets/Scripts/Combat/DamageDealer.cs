using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private bool disableHitboxOnStart = true;

    private Collider2D hitbox;

    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
        if (disableHitboxOnStart)
        {
            hitbox.enabled = false;
        }
    }

    public void SetDamage(float amount)
    {
        damage = Mathf.Max(0f, amount);
    }

    public void EnableHitbox()
    {
        if (hitbox != null)
        {
            hitbox.enabled = true;
        }
    }

    public void DisableHitbox()
    {
        if (hitbox != null)
        {
            hitbox.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        DamageReceiver receiver = other.GetComponentInParent<DamageReceiver>();
        if (receiver != null)
        {
            receiver.ApplyDamage(damage, transform.position);
        }
    }
}
