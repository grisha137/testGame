using System;
using UnityEngine;

public class ComboSystem
{
    private int comboIndex;
    private float lastAttackTime;
    private AttackData lastAttackData;

    public int CurrentComboCount { get; private set; }
    public event Action<int> ComboChanged;

    public AttackData GetNextAttack(WeaponData weaponData)
    {
        if (weaponData == null || weaponData.attacks == null || weaponData.attacks.Length == 0)
        {
            return null;
        }

        if (lastAttackData != null)
        {
            float comboWindow = Mathf.Max(0.05f, lastAttackData.comboWindow);
            if (Time.time - lastAttackTime > comboWindow)
            {
                comboIndex = 0;
                CurrentComboCount = 1;
            }
            else
            {
                comboIndex = (comboIndex + 1) % weaponData.attacks.Length;
                CurrentComboCount = Mathf.Clamp(CurrentComboCount + 1, 1, weaponData.attacks.Length);
            }
        }
        else
        {
            comboIndex = 0;
            CurrentComboCount = 1;
        }

        AttackData next = weaponData.attacks[comboIndex];
        lastAttackData = next;
        ComboChanged?.Invoke(CurrentComboCount);
        return next;
    }

    public void NotifyAttackComplete(float timeStamp)
    {
        lastAttackTime = timeStamp;
    }

    public bool IsComboExpired(float currentTime)
    {
        if (lastAttackData == null || CurrentComboCount <= 0)
        {
            return false;
        }

        float comboWindow = Mathf.Max(0.05f, lastAttackData.comboWindow);
        return currentTime - lastAttackTime > comboWindow;
    }

    public void ResetCombo()
    {
        comboIndex = 0;
        lastAttackTime = 0f;
        lastAttackData = null;
        if (CurrentComboCount != 0)
        {
            CurrentComboCount = 0;
            ComboChanged?.Invoke(CurrentComboCount);
        }
    }
}
