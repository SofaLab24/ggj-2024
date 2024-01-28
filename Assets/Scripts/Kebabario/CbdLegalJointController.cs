using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CbdLegalJointController : MonoBehaviour
{
    public AudioSource joint; // Reference to your sound effect
    public TriangleMarozController playerController;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            //jupm
            var jumpHeight = playerController.jumpHeight;
            playerController.jumpHeight = jumpHeight / 1.5f;
            playerController.Points += 0.01f;
            playerController.updateCashCounter();
            joint.Play();
            Destroy(gameObject);
        }
    }
}
