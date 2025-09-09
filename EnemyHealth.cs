using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Special Enemy Settings")]
    public bool triggersWin = false; 
    public GameObject winUIPanel;    

    private void Awake()
    {
        currentHealth = maxHealth;


        if (winUIPanel != null)
            winUIPanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        if (triggersWin && winUIPanel != null)
        {
            winUIPanel.SetActive(true);

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        Destroy(gameObject);
    }
}
