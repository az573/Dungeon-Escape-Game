using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    [SerializeField] private Animator chestAnimator;
    [SerializeField] private string openAnimationTrigger = "Open";
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactDistance = 5f; 
    [SerializeField] private float lookAngleThreshold = 45f; 

    [SerializeField] private GameObject keyPrefab;       
    [SerializeField] private GameObject heartPrefab;     
    [SerializeField] private Transform keySpawnPoint;       
    [SerializeField] private Transform heartSpawnPoint;

    private bool isOpen = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isOpen) return;

        if (Input.GetKeyDown(interactKey) && IsPlayerLookingAtChest())
        {
            chestAnimator.SetTrigger(openAnimationTrigger);
            isOpen = true;


            if (keyPrefab != null && keySpawnPoint != null)
            {
                Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
            }

            
            if (heartPrefab != null && heartSpawnPoint != null)
            {
                Vector3 offset = new Vector3(0.3f, 0, 0); 
                Instantiate(heartPrefab, heartSpawnPoint.position + offset, heartSpawnPoint.rotation);
            }
        }
    }

    private bool IsPlayerLookingAtChest()
    {
        if (player == null) return false;

        Vector3 directionToChest = (transform.position - player.position).normalized;
        float angle = Vector3.Angle(player.forward, directionToChest);

        float distance = Vector3.Distance(player.position, transform.position);
        return distance <= interactDistance && angle <= lookAngleThreshold;
    }
}
