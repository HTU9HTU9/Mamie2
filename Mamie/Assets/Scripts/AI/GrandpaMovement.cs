using UnityEngine;
using UnityEngine.AI;

public class GrandpaMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    private float switchStateTimer = 10f;  
    private bool isIdle = true;  

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 1.0f;  
        CalculateNewDestination();  
    }

    void Update()
    {
        switchStateTimer -= Time.deltaTime;  

        if (switchStateTimer <= 0)
        {
            if (isIdle)
            {
                
                CalculateNewDestination();  
                animator.ResetTrigger("Idle");  
                animator.SetTrigger("Walk");  
                isIdle = false;  
                switchStateTimer = 10f;  
            }
            else
            {
                agent.ResetPath();  
                animator.ResetTrigger("Walk");  
                animator.SetTrigger("Idle");  
                isIdle = true;  
                switchStateTimer = 10f;  
            }
        }

        
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isIdle)
        {
            CalculateNewDestination();  
        }
    }

    void CalculateNewDestination()
    {
        
        Vector3 randomDirection = Random.insideUnitSphere * 4;  
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
