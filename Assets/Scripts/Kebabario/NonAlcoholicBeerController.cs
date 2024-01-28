using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAlcoholicBeerController : MonoBehaviour
{
    // Update is called once per frame
    public TriangleMarozController playerController;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            //jupm
            playerController.BecomeDrunk();
            playerController.Points += 0.01f;
            playerController.updateCashCounter();
            Destroy(gameObject);
        }
    }
}
