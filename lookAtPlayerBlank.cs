using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform muzzlePoint;
    public GameObject projectileParent;

    public float projectileLifespan = 3f;
    public float projectileSpeed = 20f;
    public float fireRate = 1f; // Seconds between shots

    private bool playerInRange = false;
    private float fireCooldown;

    void Update()
    {
        if (playerInRange)
        {
            // Perform a line of sight check
            Vector3 directionToPlayer = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, directionToPlayer.normalized);
            RaycastHit hit;

            // Check if we can see the player
            if (Physics.Raycast(ray, out hit, directionToPlayer.magnitude))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Player is visible, so shoot
                    transform.LookAt(player.transform);

                    if (fireCooldown <= 0f)
                    {
                        FireProjectile();
                        fireCooldown = fireRate;
                    }
                }
            }
        }

        // Cooldown logic
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    //to see if the player reenter the spawn area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    //for when the player leavess the spawna srea
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
        projectile.transform.SetParent(projectileParent.transform);

        // Move the projectile straight forward 
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(muzzlePoint.forward * projectileSpeed, ForceMode.Impulse);

        Destroy(projectile, projectileLifespan);
    }
}
