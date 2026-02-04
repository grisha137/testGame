using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Data", fileName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public float healthBonus;
    public float damageBonus;
    public float moveSpeedBonus;
}
