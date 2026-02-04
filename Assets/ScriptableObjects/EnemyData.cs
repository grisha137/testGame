using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 30f;
    public float moveSpeed = 3f;
    public float damage = 5f;
    public float chaseRange = 6f;
    public float attackRange = 1.2f;
}
