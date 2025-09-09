using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damageCooldown = 1f;

    private float lastDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
        {
            GameManager.Instance.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }
}
