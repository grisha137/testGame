using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level Data", fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "New Level";
    public Vector2 playerSpawnPosition;
    public List<LevelEnemySpawn> enemySpawns = new List<LevelEnemySpawn>();
}

[System.Serializable]
public class LevelEnemySpawn
{
    public GameObject prefab;
    public Vector2 position;
}
