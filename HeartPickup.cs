using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public float interactionDistance = 3f;
    public float facingThreshold = 0.5f;
    public int healAmount = 1; 

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
                GameManager.Instance.AddHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
