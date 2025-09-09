using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator; // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
}
