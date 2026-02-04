using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data", fileName = "AttackData")]
public class AttackData : ScriptableObject
{
    public string animationName = "Attack";
    public float damage = 10f;
    public float duration = 0.3f;
    public float comboWindow = 0.2f;
}
