using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolChaseNavMesh : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Ranges")]
    public float chaseRadius = 10f;   // entra a persecución
    public float loseRadius = 14f;    // regresa a patrulla (histeresis)
    public float stopDistance = 1.6f; // distancia para detenerse cerca del jugador

    [Header("Speeds")]
    public float patrolSpeed = 2.2f;
    public float chaseSpeed = 3.8f;

    [Header("Patrol")]
    public float pointTolerance = 0.4f;

    private NavMeshAgent agent;
    private int patrolIndex = 0;

    private enum State { Patrol, Chase }
    private State state = State.Patrol;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (agent == null)
        {
            Debug.LogError("Falta NavMeshAgent en el enemigo.");
            enabled = false;
            return;
        }

        agent.stoppingDistance = stopDistance;
        agent.speed = patrolSpeed;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Cambios de estado (evita “parpadeo” con dos radios)
        if (state == State.Patrol && dist <= chaseRadius)
        {
            state = State.Chase;
            agent.speed = chaseSpeed;
        }
        else if (state == State.Chase && dist >= loseRadius)
        {
            state = State.Patrol;
            agent.speed = patrolSpeed;
            GoToNextPatrolPoint();
        }

        // Lógica por estado
        if (state == State.Chase)
        {
            agent.SetDestination(player.position);

            // Si ya está muy cerca, detente y mira al jugador
            if (dist <= stopDistance + 0.2f)
            {
                agent.ResetPath();
                FaceTarget(player.position);
            }
        }
        else // Patrol
        {
            if (patrolPoints == null || patrolPoints.Length == 0) return;

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + pointTolerance)
            {
                GoToNextPatrolPoint();
            }
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        // Encuentra un punto válido (por si alguno está null)
        int safe = 0;
        while (patrolPoints[patrolIndex] == null && safe < patrolPoints.Length)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            safe++;
        }

        if (patrolPoints[patrolIndex] != null)
            agent.SetDestination(patrolPoints[patrolIndex].position);

        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    private void FaceTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 8f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseRadius);
    }
}