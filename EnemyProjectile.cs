using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    private Vector3 direction;

    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }
    }
}
