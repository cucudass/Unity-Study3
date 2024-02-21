using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Metalon : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform turrentPosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        turrentPosition = GameObject.Find("Turret Tower").transform;
    }

    void Update()
    {
        Move();
    }

    public void Move() {
        navMeshAgent.SetDestination(turrentPosition.position);
    }
}
