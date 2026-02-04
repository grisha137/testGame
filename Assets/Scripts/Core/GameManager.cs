using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform defaultSpawnPoint;

    private Transform currentCheckpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void Respawn(PlayerController player)
    {
        if (player == null)
        {
            return;
        }

        Transform spawnPoint = currentCheckpoint != null ? currentCheckpoint : defaultSpawnPoint;
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }

        player.ResetPlayer();
    }
}
