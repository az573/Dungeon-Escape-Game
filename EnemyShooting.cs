using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;     
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float detectionRange = 10f;

    private float lastShotTime = 0f;
    private GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectionRange)
        {
            agent.isStopped = true; 
            ShootAtPlayer();
        }
        else
        {
            agent.isStopped = false; 
        }
    }

    void ShootAtPlayer()
    {
        if (Time.time - lastShotTime >= fireRate)
        {
            Vector3 dir = (player.transform.position - firePoint.position).normalized;
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            proj.GetComponent<EnemyProjectile>().Init(dir);
            lastShotTime = Time.time;
        }
    }

}
