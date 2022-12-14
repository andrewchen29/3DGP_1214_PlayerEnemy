using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling = 0,
    Tracing,
    Attacking
}

public class AINavagation : MonoBehaviour
{
    public Transform playerTransform;
    public float rotateSpeed = 1.0f;
    public float patrollSpeed = 2.0f;
    public float traceSpeed = 3.0f;
    public float playerDetectRange = 7.0f;
    public float playerAttackRange = 4.0f;

    float walkPointRangeX = 3.0f;
    float walkPointRangeZ = 1.0f;
    NavMeshAgent agent;
    Vector3 walkPoint;
    bool setWalkPoint = false;
    EnemyState state;
    Vector3 front;

    public Vector3 GetFront()
    {
        return front;
    }

    public EnemyState GetState()
    {
        return state;
    }

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        front =  new Vector3(1.0f, 0.0f, 0.0f);
        state = EnemyState.Patrolling;
    }

    public void Update()
    {
        var newFront = (walkPoint - transform.position).normalized;
        if (newFront.magnitude > 0)
            front = newFront;
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(front, Vector3.up), rotateSpeed);

        float distanceToPlayer = (playerTransform.position - transform.position).magnitude;
        if (distanceToPlayer > playerDetectRange)
            state = EnemyState.Patrolling;
        else if (distanceToPlayer > playerAttackRange)
            state = EnemyState.Tracing;
        else
            state = EnemyState.Attacking;
        switch (state)
        {
            case EnemyState.Patrolling:
            agent.speed = patrollSpeed;
                Patrolling();
                break;    
            case EnemyState.Tracing:
                walkPoint = playerTransform.position;
                agent.speed = traceSpeed;
                break;    
            case EnemyState.Attacking:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(walkPoint - transform.position, Vector3.up), 2.0f);
                walkPoint = playerTransform.position;
                agent.speed = 0.0f;
                break;    
        }
        agent.SetDestination(walkPoint);
    }

    void Patrolling()
    {
        if (!setWalkPoint)
        {
            SetWalkPoint();
            setWalkPoint = true;
        }
        float distanceToWalkPoint = (transform.position - walkPoint).magnitude;
        if (distanceToWalkPoint < 1.0f)
            SetWalkPoint();
    }

    void SetWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRangeZ, walkPointRangeZ);
        float randomX = Random.Range(0.0f, walkPointRangeX);
        Vector3 right = Vector3.Cross(front, Vector3.up);
        walkPoint = transform.position + right * randomZ + front * randomX;

        NavMeshHit hit;
        NavMesh.SamplePosition(walkPoint, out hit, 2.0f, 1);
        if ((!hit.hit) || (hit.position - transform.position).magnitude < 0.3f)
            // walkPoint = transform.position - (hit.position - transform.position) * walkPointRangeX;
            walkPoint = transform.position - right * randomZ - front * randomX * 0.5f;
        else
            walkPoint = hit.position;
    }
}
