using UnityEngine;
using UnityEngine.AI;

public class EnemyStaticChaseNavMesh : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Ranges")]
    public float chaseRadius = 10f;
    public float stopDistance = 1.6f;

    [Header("Speed")]
    public float chaseSpeed = 3.8f;

    private NavMeshAgent agent;

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
        agent.speed = chaseSpeed;

        // Arranca quieto
        agent.ResetPath();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= chaseRadius)
        {
            agent.SetDestination(player.position);

            if (dist <= stopDistance + 0.2f)
            {
                agent.ResetPath();
                FaceTarget(player.position);
            }
        }
        else
        {
            // Fuera del radio: quieto
            agent.ResetPath();
        }
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
    }
}