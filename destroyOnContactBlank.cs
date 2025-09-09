using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnContactBlank : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitBox"))
        {
            EnemyHealth enemyHealth = other.transform.root.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                return;
            }
            BossHealth boss = other.GetComponent<BossHealth>();
             if (boss != null)
             {
                 boss.TakeDamage(1);
             }
            Destroy(gameObject);
        }
        
    }
}

