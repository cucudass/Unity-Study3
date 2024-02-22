using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { MOVE, ATTACK, DIE }
public class Metalon : MonoBehaviour
{
    private int health;
    public int Health {
        set { health = value; }
        get { return health; }
    }

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    [SerializeField] Transform turrentPosition;
    [SerializeField] State state;

    void Start()
    {
        health = 100;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        turrentPosition = GameObject.Find("Turret Tower").transform;
    }

    void Update()
    {
        switch (state) {
            case State.MOVE:
                Move();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.DIE:
                Die();
                break;
        }
    }

    public void Move() {
        navMeshAgent.SetDestination(turrentPosition.position);
    }

    public void Attack() {
        animator.SetBool("Attack", true);
    }

    public void Die() {
        animator.Play("Die");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Turret Tower")) {
            state = State.ATTACK;
        }
    }
}
