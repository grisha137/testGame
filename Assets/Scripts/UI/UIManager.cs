using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ComboCounter comboCounter;

    private HealthSystem playerHealth;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthSystem>();
        }

        if (playerHealth != null)
        {
            playerHealth.HealthChanged += OnHealthChanged;
            OnHealthChanged(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(float current, float max)
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(current, max);
        }
    }

    public void SetComboCount(int combo)
    {
        if (comboCounter != null)
        {
            comboCounter.SetCombo(combo);
        }
    }
}
