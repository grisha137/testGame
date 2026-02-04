using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    public float MaxHealth => maxHealth;
    public float CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0f;

    public event Action<float, float> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void Initialize(float newMaxHealth)
    {
        maxHealth = Mathf.Max(1f, newMaxHealth);
        CurrentHealth = maxHealth;
        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (IsDead)
        {
            return;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth - Mathf.Abs(amount), 0f, maxHealth);
        HealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (IsDead)
        {
            Died?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead)
        {
            return;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth + Mathf.Abs(amount), 0f, maxHealth);
        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
}
