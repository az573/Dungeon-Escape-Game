using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator animator;
    public string isOpenBoolName = "IsOpen";
    public float interactionDistance = 3f;
    public float facingThreshold = 0.5f;

    [Header("Door Settings")]
    public bool requiresKey = false;      
    public string requiredTag = "Locked"; 

    private Transform player;
    private bool unlocked = false;        

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toDoor = (transform.position - player.position);
        float distance = toDoor.magnitude;

        if (distance <= interactionDistance)
        {
            toDoor.Normalize();
            float facing = Vector3.Dot(player.forward, toDoor);

            if (facing > facingThreshold && Input.GetKeyDown(KeyCode.E))
            {
                TryOpenDoor();
            }
        }
    }

    void TryOpenDoor()
    {
        if (unlocked)
        {
           
            ToggleDoor();
            return;
        }

        // If door requires a key, check inventory
        if (requiresKey && CompareTag(requiredTag))
        {
            if (GameManager.Instance.keysCollected > 0)
            {
                GameManager.Instance.UseKey();
                unlocked = true; 
                ToggleDoor();
            }
            else
            {
                Debug.Log("This door is locked. You need a key!");
            }
        }
        else
        {
            ToggleDoor();
        }
    }

    void ToggleDoor()
    {
        bool isOpen = animator.GetBool(isOpenBoolName);
        animator.SetBool(isOpenBoolName, !isOpen); 
    }
}
