using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Distance at which the AI detects the player
    [SerializeField] private float detectionRange = 15f;

    // Speed at which the AI rotates
    [SerializeField] private float lookSpeed = 5f;

    // How far the AI can roam from its current position
    [SerializeField] private float patrolRadius = 10f;

    // Time to wait before picking a new patrol point
    [SerializeField] private float patrolWaitTime = 2f;   

    private NavMeshAgent agent;
    private Vector3 patrolTarget;
    private float patrolTimer;

    // Initialize the NavMeshAgent and set the first patrol point
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disable automatic rotation and up-axis updates so we can control rotation manually
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        SetNewPatrolPoint();
    }

    // Handles switching between patrol and tracking the player
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            // Player detected — stop moving
            agent.SetDestination(transform.position);

            // Face the player smoothly
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Ignore vertical difference

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
            }
        }
        else
        {
            Patrol();
        }
    }

    // Controls enemy patrolling logic
    void Patrol()
    {
        // Rotate to face the direction of movement
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 direction = agent.velocity.normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
            }
        }

        // If the enemey reaches the current patrol point, wait then choose a new one
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                SetNewPatrolPoint();
                patrolTimer = 0f;
            }
        }
    }

    // Picks a new random point within patrol radius on the NavMesh
    void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = navHit.position;
            agent.SetDestination(patrolTarget);
        }
    }
}
