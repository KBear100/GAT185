using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Sensor sensor;
    [SerializeField] Transform attackTransform;
    [SerializeField] float damage;

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

    IEnumerator Attack() 
    { 
        state = State.ATTACK; 
        animator.SetTrigger("Attack"); 
        yield return new WaitForSeconds(4.0f); 
        state = State.CHASE; 
    }

    IEnumerator Death()
    {
        state = State.DEATH;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }

    void OnAnimAttack()
    {
        Debug.Log("Attack");

        var colliders = Physics.OverlapSphere(attackTransform.position, 2);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.TryGetComponent<Health>(out Health health))
                {
                    health.OnApplyDamage(damage);
                    break;
                }
            }
        }
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
                if(sensor.sensed != null)
                {
                    state = State.CHASE;
                }
                break;
            case State.CHASE:
                navMeshAgent.isStopped = false;
                if(sensor.sensed != null)
                {
                    target = sensor.sensed.transform;
                    float distance = Vector3.Distance(target.position, transform.position);
                    Debug.Log(distance);
                    if(distance <= 2)
                    {
                        StartCoroutine(Attack());
                    }
                    timer = 2;
                }
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    state = State.PATROL;
                }
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
