using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Weapon Data", fileName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public AttackData[] attacks;
    public float baseDamage = 10f;
}
