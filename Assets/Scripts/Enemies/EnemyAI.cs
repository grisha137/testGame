using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 1f;

    private EnemyController controller;
    private int patrolIndex;
    private float nextMoveTime;
    private Transform player;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (controller.Health.IsDead || controller.Data == null)
        {
            return;
        }

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= controller.Data.attackRange)
            {
                controller.Attack();
                return;
            }

            if (distance <= controller.Data.chaseRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                controller.Move(direction);
                return;
            }
        }

        Patrol();
    }

    private void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            controller.Move(Vector2.zero);
            return;
        }

        if (Time.time < nextMoveTime)
        {
            controller.Move(Vector2.zero);
            return;
        }

        Transform target = patrolPoints[patrolIndex];
        Vector2 direction = (target.position - transform.position);
        if (direction.magnitude <= 0.2f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            nextMoveTime = Time.time + waitTime;
            controller.Move(Vector2.zero);
            return;
        }

        controller.Move(direction.normalized);
    }
}
