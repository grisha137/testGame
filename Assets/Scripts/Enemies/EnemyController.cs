using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthSystem))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private AnimationController animationController;

    private Rigidbody2D body;
    private HealthSystem health;

    public EnemyData Data => enemyData;
    public HealthSystem Health => health;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthSystem>();

        if (enemyData != null)
        {
            health.Initialize(enemyData.maxHealth);
        }

        health.Died += OnDeath;
    }

    public void Move(Vector2 direction)
    {
        if (health.IsDead || enemyData == null)
        {
            return;
        }

        body.velocity = new Vector2(direction.x * enemyData.moveSpeed, body.velocity.y);
        if (animationController != null)
        {
            animationController.SetBool("Moving", Mathf.Abs(direction.x) > 0.05f);
        }
    }

    public void Attack()
    {
        if (enemyData == null)
        {
            return;
        }

        if (damageDealer != null)
        {
            damageDealer.SetDamage(enemyData.damage);
            damageDealer.EnableHitbox();
        }

        if (animationController != null)
        {
            animationController.Play("Attack");
        }
    }

    public void StopAttack()
    {
        if (damageDealer != null)
        {
            damageDealer.DisableHitbox();
        }
    }

    private void OnDeath()
    {
        if (animationController != null)
        {
            animationController.Play("Death");
        }

        if (damageDealer != null)
        {
            damageDealer.DisableHitbox();
        }
    }
}
