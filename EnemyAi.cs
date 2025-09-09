using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform[] Waypoints;
    [SerializeField] int currentWaypoints = 0;
    [SerializeField] float distanceThreshold = 0.5f;
    [SerializeField] GameObject player;

    [SerializeField] int damageAmount = 1;        
    [SerializeField] float damageCooldown = 1f;   

    private float lastDamageTime = 0f;

    NavMeshAgent _navMeshAgent;

    public enum EnemyState
    {
        patrol,
        chase
    }

    public EnemyState currentState;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = EnemyState.patrol;
        SetEnemyWaypoint(Waypoints[currentWaypoints].position);
    }

    void Update()
    {
        if (currentState == EnemyState.patrol)
        {
            Patrol();
        }
        else if (currentState == EnemyState.chase)
        {
            Chase();
        }

        
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= _navMeshAgent.stoppingDistance + 0.5f)  
            {
                TryDamagePlayer();
            }
        }
    }

    void Patrol()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < distanceThreshold)
        {
            ChangeWaypoint();
        }
    }

    void Chase()
    {
        if (player != null)
            SetEnemyWaypoint(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.chase;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.patrol;
            player = null;
            SetEnemyWaypoint(Waypoints[currentWaypoints].position);
        }
    }

    void ChangeWaypoint()
    {
        if (currentWaypoints < Waypoints.Length - 1)
        {
            currentWaypoints++;
        }
        else
        {
            currentWaypoints = 0;
        }
        SetEnemyWaypoint(Waypoints[currentWaypoints].position);
    }

    void SetEnemyWaypoint(Vector3 WaypointPosition)
    {
        _navMeshAgent.SetDestination(WaypointPosition);
    }

    void TryDamagePlayer()
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            GameManager.Instance.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }
}
