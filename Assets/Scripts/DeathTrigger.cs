using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    // --------------------------------------------------------------

    // Events
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    // --------------------------------------------------------------

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if(playerController)
            {
                // Kill the player
                playerController.Die();

                // Send the PlayerDeath event
                if(OnPlayerDeath != null)
                {
                    OnPlayerDeath();
                }
            }
        }
    }
}
