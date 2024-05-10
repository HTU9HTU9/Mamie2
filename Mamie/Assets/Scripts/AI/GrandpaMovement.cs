using UnityEngine;
using UnityEngine.AI;

public class GrandpaMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public NavMeshAgent agent;
    public Animator animator;

    private Vector3 destination;
    private bool isWalking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        CalculateNewDestination();
    }

    void Update() {
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
        isWalking = false;
        CalculateNewDestination();
    } else {
        isWalking = true;
    }
    animator.SetBool("IsWalking", isWalking);
    Debug.Log("Agent moving: " + agent.velocity.magnitude);
}

    void CalculateNewDestination() {
    Vector3 randomDirection = Random.insideUnitSphere * 20;
    randomDirection += transform.position;
    NavMeshHit hit;
    if (NavMesh.SamplePosition(randomDirection, out hit, 20, NavMesh.AllAreas)) {
        destination = hit.position;
        agent.SetDestination(destination);
        Debug.Log("New destination set: " + destination);
    } else {
        Debug.Log("Failed to find a valid destination.");
    }
}
}
