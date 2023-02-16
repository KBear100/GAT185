using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPlayer : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);

        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    if(Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        navMeshAgent.SetDestination(hit.point);
        //    }
        //}
    }
}
