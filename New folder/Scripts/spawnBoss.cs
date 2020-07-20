using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss : MonoBehaviour
{
    public GameObject makeBoss;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            makeBoss.SetActive(true);
        }
    }
}
