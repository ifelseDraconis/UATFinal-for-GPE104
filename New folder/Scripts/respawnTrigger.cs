using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnTrigger : MonoBehaviour
{
    public GameObject thePlayerController;
    public pcController thisBoss;

    public GameObject thisRespawnPoint;
    public Transform thisRespawnLocation;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // code here to change the player's respawn to here when the player runs over this trigger
            thisBoss.reSpawnLocation = thisRespawnLocation.localPosition;
        }

    }
}
