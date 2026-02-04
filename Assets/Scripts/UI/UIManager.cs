using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ComboCounter comboCounter;

    private HealthSystem playerHealth;
    private WeaponController playerWeapon;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthSystem>();
            playerWeapon = player.GetComponentInChildren<WeaponController>();
        }

        if (playerHealth != null)
        {
            playerHealth.HealthChanged += OnHealthChanged;
            OnHealthChanged(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }

        if (playerWeapon != null)
        {
            playerWeapon.ComboChanged += OnComboChanged;
            OnComboChanged(playerWeapon.CurrentComboCount);
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= OnHealthChanged;
        }

        if (playerWeapon != null)
        {
            playerWeapon.ComboChanged -= OnComboChanged;
        }
    }

    private void OnHealthChanged(float current, float max)
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(current, max);
        }
    }

    private void OnComboChanged(int combo)
    {
        if (comboCounter != null)
        {
            comboCounter.SetCombo(combo);
        }
    }
}
