using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float interactionDistance = 3f;
    public float facingThreshold = 0.5f;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toItem = transform.position - player.position;
        float distance = toItem.magnitude;

        if (distance <= interactionDistance)
        {
            toItem.Normalize();
            float facing = Vector3.Dot(player.forward, toItem);

            if (facing > facingThreshold && Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.AddKey();
                Destroy(gameObject);
            }
        }
    }
}
