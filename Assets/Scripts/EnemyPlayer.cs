using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;

    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    private Transform target;

    private State state = State.IDLE;
    private float timer = 0;

    enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DEATH
    }

    void OnDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetComponent<Health>().onDeath += OnDeath;
    }

    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                state = State.PATROL;
                break;
            case State.PATROL:
                navMeshAgent.isStopped = false;
                target = GetComponent<WaypointNavagator>().waypoint.transform;
                break;
            case State.CHASE:
                navMeshAgent.isStopped = false;
                break;
            case State.ATTACK:
                navMeshAgent.isStopped = true;
                break;
            case State.DEATH:
                navMeshAgent.isStopped = true;
                break;
            default:
                break;
        }

        navMeshAgent.SetDestination(target.position);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
}
