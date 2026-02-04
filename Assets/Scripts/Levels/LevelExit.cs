using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private int levelIndex = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelManager.Instance == null)
        {
            return;
        }

        if (other.GetComponentInParent<PlayerController>() == null)
        {
            return;
        }

        if (levelIndex >= 0)
        {
            LevelManager.Instance.LoadLevel(levelIndex);
        }
        else
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}
