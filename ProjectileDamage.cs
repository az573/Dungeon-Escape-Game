using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.collider.CompareTag("Player"))
        {
            hasHit = true;
           


            Destroy(gameObject); 
        }
    }
}



