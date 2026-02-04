using UnityEngine;

public class ComboSystem
{
    private int comboIndex;
    private float lastAttackTime;
    private AttackData lastAttackData;

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
            }
            else
            {
                comboIndex = (comboIndex + 1) % weaponData.attacks.Length;
            }
        }

        AttackData next = weaponData.attacks[comboIndex];
        lastAttackData = next;
        return next;
    }

    public void NotifyAttackComplete(float timeStamp)
    {
        lastAttackTime = timeStamp;
    }

    public void ResetCombo()
    {
        comboIndex = 0;
        lastAttackTime = 0f;
        lastAttackData = null;
    }
}
