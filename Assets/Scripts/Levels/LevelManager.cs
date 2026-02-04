using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private List<LevelData> levels = new List<LevelData>();
    [SerializeField] private int startingLevelIndex;
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private PlayerController existingPlayer;
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform enemiesParent;

    private readonly List<GameObject> spawnedEnemies = new List<GameObject>();
    private int currentLevelIndex = -1;
    private PlayerController playerInstance;

    public int CurrentLevelIndex => currentLevelIndex;
    public LevelData CurrentLevel => currentLevelIndex >= 0 && currentLevelIndex < levels.Count ? levels[currentLevelIndex] : null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (levels.Count > 0)
        {
            LoadLevel(Mathf.Clamp(startingLevelIndex, 0, levels.Count - 1));
        }
    }

    public void LoadNextLevel()
    {
        if (levels.Count == 0)
        {
            return;
        }

        int nextIndex = Mathf.Clamp(currentLevelIndex + 1, 0, levels.Count - 1);
        LoadLevel(nextIndex);
    }

    public void LoadLevel(int index)
    {
        if (levels.Count == 0)
        {
            return;
        }

        int clampedIndex = Mathf.Clamp(index, 0, levels.Count - 1);
        currentLevelIndex = clampedIndex;
        ClearLevel();

        LevelData level = levels[clampedIndex];
        SpawnPlayer(level);
        SpawnEnemies(level);
    }

    public void ReloadCurrentLevel()
    {
        if (currentLevelIndex < 0)
        {
            return;
        }

        LoadLevel(currentLevelIndex);
    }

    private void ClearLevel()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] != null)
            {
                Destroy(spawnedEnemies[i]);
            }
        }

        spawnedEnemies.Clear();
    }

    private void SpawnPlayer(LevelData level)
    {
        if (level == null)
        {
            return;
        }

        if (playerInstance == null && existingPlayer != null)
        {
            playerInstance = existingPlayer;
        }

        if (playerInstance == null && playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, level.playerSpawnPosition, Quaternion.identity, playerParent);
        }

        if (playerInstance != null)
        {
            playerInstance.transform.position = level.playerSpawnPosition;
            playerInstance.ResetPlayer();
        }
    }

    private void SpawnEnemies(LevelData level)
    {
        if (level == null || level.enemySpawns == null)
        {
            return;
        }

        foreach (LevelEnemySpawn spawn in level.enemySpawns)
        {
            if (spawn == null || spawn.prefab == null)
            {
                continue;
            }

            GameObject enemy = Instantiate(spawn.prefab, spawn.position, Quaternion.identity, enemiesParent);
            spawnedEnemies.Add(enemy);
        }
    }
}
