using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private ComboSystem comboSystem;

    private float attackEndTime;

    public bool IsAttacking { get; private set; }
    public int CurrentComboCount => comboSystem != null ? comboSystem.CurrentComboCount : 0;
    public event Action<int> ComboChanged;

    private void Awake()
    {
        if (comboSystem == null)
        {
            comboSystem = new ComboSystem();
        }

        comboSystem.ComboChanged += HandleComboChanged;
    }

    private void OnDestroy()
    {
        if (comboSystem != null)
        {
            comboSystem.ComboChanged -= HandleComboChanged;
        }
    }

    private void Update()
    {
        if (IsAttacking && Time.time >= attackEndTime)
        {
            FinishAttack();
        }

        if (!IsAttacking && comboSystem != null && comboSystem.IsComboExpired(Time.time))
        {
            comboSystem.ResetCombo();
        }
    }

    public void StartAttack()
    {
        if (IsAttacking)
        {
            return;
        }

        AttackData data = comboSystem.GetNextAttack(weaponData);
        if (data == null)
        {
            return;
        }

        IsAttacking = true;
        attackEndTime = Time.time + data.duration;

        if (animationController != null && !string.IsNullOrEmpty(data.animationName))
        {
            animationController.Play(data.animationName);
        }

        if (damageDealer != null)
        {
            float damage = data.damage > 0f ? data.damage : weaponData != null ? weaponData.baseDamage : 1f;
            damageDealer.SetDamage(damage);
            damageDealer.EnableHitbox();
        }
    }

    public void ResetCombo()
    {
        if (comboSystem != null)
        {
            comboSystem.ResetCombo();
        }

        if (damageDealer != null)
        {
            damageDealer.DisableHitbox();
        }

        IsAttacking = false;
    }

    private void FinishAttack()
    {
        IsAttacking = false;
        if (damageDealer != null)
        {
            damageDealer.DisableHitbox();
        }

        comboSystem.NotifyAttackComplete(Time.time);
    }

    private void HandleComboChanged(int comboCount)
    {
        ComboChanged?.Invoke(comboCount);
    }
}
